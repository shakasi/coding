using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示一个构造函数调用
    /// </summary>
	public class NewExpressionProvider : ExpressionToSqlBase<NewExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator Select(SqlCreator sqlCreator, NewExpression expression)
        {
            for (var i = 0; i < expression.Members.Count; i++)
            {
                var argument = expression.Arguments[i];
                var member = expression.Members[i];
                sqlCreator.Select(argument);
                //添加字段别名
                if (argument is MemberExpression memberExpression && memberExpression.Member.Name != member.Name)
                    sqlCreator.SelectFields[sqlCreator.SelectFields.Count - 1] += " AS " + member.Name;
            }
            return sqlCreator;
        }

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, NewExpression expression)
        {
            foreach (Expression item in expression.Arguments)
            {
                sqlCreator.GroupBy(item);
                sqlCreator += ",";
            }
            sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
            return sqlCreator;
        }

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, NewExpression expression, params OrderType[] orders)
        {
            for (var i = 0; i < expression.Arguments.Count; i++)
            {
                sqlCreator.OrderBy(expression.Arguments[i]);
                if (i <= orders.Length - 1)
                    sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                else
                    sqlCreator += " ASC,";
            }
            sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
            return sqlCreator;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, NewExpression expression)
        {
            if (expression.NodeType == ExpressionType.New && expression.Type.GetNonNullableType() == typeof(DateTime))
            {
                //if (sqlCreator.DatabaseType == DatabaseType.Oracle)
                //{
                //    sqlCreator += $"TO_DATE('{expression?.ToString()}', 'yyyy-mm-dd hh24:mi:ss')";
                //}
                //else
                //{
                //    sqlCreator += expression?.ToString();
                //}                
                sqlCreator.AddDbParameter(expression.ToObject());
            }
         
            return sqlCreator;
        }
        #endregion
    }
}