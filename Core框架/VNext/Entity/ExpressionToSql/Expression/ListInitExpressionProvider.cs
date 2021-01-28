using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VNext.Entity;

namespace VNext.Entity
{
    /// <summary>
    /// 表示包含集合初始值设定项的构造函数调用
    /// </summary>
    public class ListInitExpressionProvider : ExpressionToSqlBase<ListInitExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, ListInitExpression expression)
        {
            var array = (expression.ToObject() as IEnumerable<object>)?.ToList();
            if (array != null)
            {
                for (var i = 0; i < array.Count; i++)
                {
                    sqlCreator.GroupBy(Expression.Constant(array[i], array[i].GetType()));
                }
                sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
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
        public override SqlCreator OrderBy(SqlCreator sqlCreator, ListInitExpression expression, params OrderType[] orders)
        {
            var array = (expression.ToObject() as IEnumerable<object>)?.ToList();
            if (array != null)
            {
                for (var i = 0; i < array.Count; i++)
                {
                    sqlCreator.OrderBy(Expression.Constant(array[i], array[i].GetType()));
                    if (i <= orders.Length - 1)
                    {
                        sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                    }
                    else if (!array[i].ToString().ToUpper().Contains("ASC") && !array[i].ToString().ToUpper().Contains("DESC"))
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
            return sqlCreator;
        }
        #endregion
    }
}