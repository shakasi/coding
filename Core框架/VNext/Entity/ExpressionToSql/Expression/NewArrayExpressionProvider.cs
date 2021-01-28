using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示创建一个新数组，并可能初始化该新数组的元素
    /// </summary>
	public class NewArrayExpressionProvider : ExpressionToSqlBase<NewArrayExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator In(SqlCreator sqlCreator, NewArrayExpression expression)
        {
            sqlCreator += "(";
            foreach (Expression expressionItem in expression.Expressions)
            {
                sqlCreator.In(expressionItem);
                sqlCreator += ",";
            }
            if (sqlCreator.SqlString[sqlCreator.SqlString.Length - 1] == ',')
                sqlCreator.SqlString.Remove(sqlCreator.SqlString.Length - 1, 1);
            sqlCreator += ")";
            return sqlCreator;
        }

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, NewArrayExpression expression)
        {
            for (var i = 0; i < expression.Expressions.Count; i++)
            {
                sqlCreator.GroupBy(expression.Expressions[i]);
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
        /// <returns>SqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, NewArrayExpression expression, params OrderType[] orders)
        {
            for (var i = 0; i < expression.Expressions.Count; i++)
            {
                sqlCreator.OrderBy(expression.Expressions[i]);
                if (i <= orders.Length - 1)
                {
                    sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                }
                else if (expression.Expressions[i] is ConstantExpression order)
                {
                    if (!order.Value.ToString().ToUpper().Contains("ASC") && !order.Value.ToString().ToUpper().Contains("DESC"))
                        sqlCreator += " ASC,";
                    else
                        sqlCreator += ",";
                }
                else
                {
                    sqlCreator += " ASC,";
                }
            }
            sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
            return sqlCreator;
        }
        #endregion
    }
}