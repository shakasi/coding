using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示访问字段或属性
    /// </summary>
	public class MemberExpressionProvider : ExpressionToSqlBase<MemberExpression>
    {
        private string GetFormatField(SqlCreator sqlCreator, MemberExpression expression)
        {
            //expression.Expression.Type为当前类类型; 
            //如果类有继承关系时,expression.Member.DeclaringType有可能为父类中定义的类型
            var type = expression.Expression.Type != expression.Member.DeclaringType ?
                       expression.Expression.Type : expression.Member.DeclaringType;

            var tableAlias = sqlCreator.GetEntityInfo(type).tableAliasName;
            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";

            var fieldTemp = $"{tableAlias}{sqlCreator.GetColumnInfo(type, expression.Member)}";
            return fieldTemp;
        }

        #region Override Base Class Methods
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Select(SqlCreator sqlCreator, MemberExpression expression)
        {
            var formatField = GetFormatField(sqlCreator, expression);
            sqlCreator.SelectFields.Add(formatField);
            return sqlCreator;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Join(SqlCreator sqlCreator, MemberExpression expression)
        {
            var formatField = GetFormatField(sqlCreator, expression);
            sqlCreator += formatField;
            return sqlCreator;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, MemberExpression expression)
        {
            //此处判断expression的Member是否是可空值类型
            if (expression.Expression.NodeType == ExpressionType.MemberAccess && expression.Member.DeclaringType.IsNullableType())
            {
                expression = expression.Expression as MemberExpression;
            }
            if (expression != null)
            {
                if (expression.Expression.NodeType == ExpressionType.Parameter)
                {
                    sqlCreator += GetFormatField(sqlCreator, expression);

                    //字段是bool类型
                    if (expression.NodeType == ExpressionType.MemberAccess && expression.Type.GetUnNullableType() == typeof(bool))
                    {
                        sqlCreator += " = 1";
                    }
                }
                else
                {
                    sqlCreator.AddDbParameter(expression.ToObject());
                }
            }
            return sqlCreator;
        }

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator In(SqlCreator sqlCreator, MemberExpression expression)
        {
            var obj = expression.ToObject();
            if (obj is IEnumerable array)
            {
                sqlCreator += "(";
                foreach (var item in array)
                {
                    ExpressionToSqlProvider.In(sqlCreator, Expression.Constant(item));
                    sqlCreator += ",";
                }
                if (sqlCreator.SqlString[sqlCreator.SqlString.Length - 1] == ',')
                {
                    sqlCreator.SqlString.Remove(sqlCreator.SqlString.Length - 1, 1);
                }
                sqlCreator += ")";
            }
            return sqlCreator;
        }

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, MemberExpression expression)
        {
            Type type = null;
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                type = expression.Expression.Type != expression.Member.DeclaringType ?
                           expression.Expression.Type :
                           expression.Member.DeclaringType;
            }
            else if (expression.Expression.NodeType == ExpressionType.Constant)
            {
                type = sqlCreator.CreatorType;
            }

            string tableAlias = sqlCreator.GetEntityInfo(type).tableAliasName;
            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                sqlCreator += tableAlias + sqlCreator.GetColumnInfo(expression.Member.DeclaringType, expression.Member);
            }
            if (expression.Expression.NodeType == ExpressionType.Constant)
            {
                var obj = expression.ToObject();
                if (obj != null)
                {
                    var objType = obj.GetType().Name;
                    if (objType == "String[]" && obj is string[] array)
                    {
                        foreach (var item in array)
                        {
                            sqlCreator.GroupBy(Expression.Constant(item, item.GetType()));
                        }
                        sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                    }
                    if (objType == "List`1" && obj is List<string> list)
                    {
                        foreach (var item in list)
                        {
                            sqlCreator.GroupBy(Expression.Constant(item, item.GetType()));
                        }
                        sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                    }
                    if (objType == "String" && obj is string str)
                    {
                        sqlCreator.GroupBy(Expression.Constant(str, str.GetType()));
                        sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                    }
                }
            }
            return sqlCreator;
        }

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, MemberExpression expression, params OrderType[] orders)
        {
            Type type = null;
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                type = expression.Expression.Type != expression.Member.DeclaringType ?
                           expression.Expression.Type :
                           expression.Member.DeclaringType;
            }
            else if (expression.Expression.NodeType == ExpressionType.Constant)
            {
                type = sqlCreator.CreatorType;
            }

            string tableAlias = sqlCreator.GetEntityInfo(type).tableAliasName;

            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                sqlCreator += tableAlias + sqlCreator.GetColumnInfo(expression.Member.DeclaringType, expression.Member);
                if (orders?.Length > 0)
                    sqlCreator += $" { (orders[0] == OrderType.Descending ? "DESC" : "ASC")}";
            }
            if (expression.Expression.NodeType == ExpressionType.Constant)
            {
                var obj = expression.ToObject();
                if (obj != null)
                {
                    var objType = obj.GetType().Name;
                    if (objType == "String[]" && obj is string[] array)
                    {
                        for (var i = 0; i < array.Length; i++)
                        {
                            sqlCreator.OrderBy(Expression.Constant(array[i], array[i].GetType()));
                            if (i <= orders.Length - 1)
                            {
                                sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                            }
                            else if (!array[i].ToUpper().Contains("ASC") && !array[i].ToUpper().Contains("DESC"))
                            {
                                sqlCreator += " ASC,";
                            }
                            else
                            {
                                sqlCreator += ",";
                            }
                        }
                        sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                    }
                    if (objType == "List`1" && obj is List<string> list)
                    {
                        for (var i = 0; i < list.Count; i++)
                        {
                            sqlCreator.OrderBy(Expression.Constant(list[i], list[i].GetType()));
                            if (i <= orders.Length - 1)
                            {
                                sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                            }
                            else if (!list[i].ToUpper().Contains("ASC") && !list[i].ToUpper().Contains("DESC"))
                            {
                                sqlCreator += " ASC,";
                            }
                            else
                            {
                                sqlCreator += ",";
                            }
                        }
                        sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                    }
                    if (objType == "String" && obj is string str)
                    {
                        sqlCreator.OrderBy(Expression.Constant(str, str.GetType()));
                        str = str.ToUpper();
                        if (!str.Contains("ASC") && !str.Contains("DESC"))
                        {
                            if (orders.Length >= 1)
                            {
                                sqlCreator += $" { (orders[0] == OrderType.Descending ? "DESC" : "ASC")},";
                            }
                            else
                            {
                                sqlCreator += " ASC,";
                            }
                            sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
                        }
                    }
                }
            }
            return sqlCreator;
        }
        #endregion
    }
}