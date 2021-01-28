using System.Linq.Expressions;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示具有常数值的表达式
    /// </summary>
	public class ConstantExpressionProvider : ExpressionToSqlBase<ConstantExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Select(SqlCreator sqlCreator, ConstantExpression expression)
        {
            if (expression.Value == null)
            {
                string tableAlias = sqlCreator.GetEntityInfo(sqlCreator.CreatorType).tableAliasName;
                if (!tableAlias.IsNullOrEmpty())
                    tableAlias += ".";
                sqlCreator.SelectFields.Add($"{tableAlias}*");
            }
            else
            {
                sqlCreator.SelectFields.Add(expression.Value.ToString());
            }
            return sqlCreator;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, ConstantExpression expression)
        {
            //表达式左侧为bool类型常量
            if (expression.NodeType == ExpressionType.Constant && expression.Value is bool b)
            {
                var sql = sqlCreator.ToString().ToUpper().Trim();
                if (!b && (sql.EndsWith("WHERE") || sql.EndsWith("AND") || sql.EndsWith("OR")))
                {
                    sqlCreator += " 1 = 0 ";
                }
            }
            else
            {
                sqlCreator.AddDbParameter(expression.Value);
            }
            return sqlCreator;
        }

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator In(SqlCreator sqlCreator, ConstantExpression expression)
        {
            sqlCreator.AddDbParameter(expression.Value);
            return sqlCreator;
        }

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, ConstantExpression expression)
        {
            string tableAlias = sqlCreator.GetEntityInfo(sqlCreator.CreatorType).tableAliasName;
            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";
            sqlCreator += tableAlias + sqlCreator.GetFormatName(expression.Value.ToString()) + ",";
            return sqlCreator;
        }

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, ConstantExpression expression, params OrderType[] orders)
        {
            string tableAlias = sqlCreator.GetEntityInfo(sqlCreator.CreatorType).tableAliasName;
            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";
            var field = expression.Value.ToString();
            if (!field.ToUpper().Contains(" ASC") && !field.ToUpper().Contains(" DESC"))
                field = sqlCreator.GetFormatName(field);
            sqlCreator += tableAlias + field;
            if (orders?.Length > 0)
                sqlCreator += $" { (orders[0] == OrderType.Descending ? "DESC" : "ASC")}";
            return sqlCreator;
        }
        #endregion
    }
}