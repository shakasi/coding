using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示具有一元运算符的表达式
    /// </summary>
	public class UnaryExpressionProvider : ExpressionToSqlBase<UnaryExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Select(SqlCreator sqlCreator, UnaryExpression expression)
        {
            sqlCreator.Select(expression.Operand);
            return sqlCreator;
        }


        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator,UnaryExpression expression)
        {
            var startIndex = sqlCreator.Length;
            sqlCreator.Where(expression.Operand);
            if (expression.NodeType == ExpressionType.Not)
            {
                var subString = sqlCreator.ToString().Substring(startIndex, sqlCreator.ToString().Length - startIndex).ToUpper();

                //IS NOT、IS                     
                if (subString.Contains("IS NOT"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("IS NOT");
                    if (index != -1) sqlCreator.SqlString.Replace("IS NOT", "IS", index, 6);
                }
                if (subString.Contains(" IS ") && subString.LastIndexOf(" IS ") != subString.LastIndexOf(" IS NOT"))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" IS ");
                    if (index != -1) sqlCreator.SqlString.Replace(" IS ", " IS NOT ", index, 4);
                }

                //NOT LIKE、LIKE
                if (subString.Contains("NOT LIKE"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("NOT LIKE");
                    if (index != -1) sqlCreator.SqlString.Replace("NOT LIKE", "LIKE", index, 8);
                }
                if (subString.Contains("LIKE") && subString.LastIndexOf("LIKE") != (subString.LastIndexOf("NOT LIKE") + 4))
                {
                    var index = sqlCreator.ToString().LastIndexOf("LIKE");
                    if (index != -1) sqlCreator.SqlString.Replace("LIKE", "NOT LIKE", index, 4);
                }

                //NOT IN、IN
                if (subString.Contains("NOT IN"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("NOT IN");
                    if (index != -1) sqlCreator.SqlString.Replace("NOT IN", "IN", index, 6);
                }
                if (subString.Contains("IN") && subString.LastIndexOf("IN") != (subString.LastIndexOf("NOT IN") + 4))
                {
                    var index = sqlCreator.ToString().LastIndexOf("IN");
                    if (index != -1) sqlCreator.SqlString.Replace("IN", "NOT IN", index, 2);
                }

                //AND、OR
                if (subString.Contains("AND"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("AND");
                    if (index != -1) sqlCreator.SqlString.Replace("AND", "OR", index, 3);
                }
                if (subString.Contains("OR"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("OR");
                    if (index != -1) sqlCreator.SqlString.Replace("OR", "AND", index, 2);
                }

                //=、<>
                if (subString.Contains(" = "))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" = ");
                    if (index != -1) sqlCreator.SqlString.Replace(" = ", " <> ", index, 3);
                }
                if (subString.Contains("<>"))
                {
                    var index = sqlCreator.ToString().LastIndexOf("<>");
                    if (index != -1) sqlCreator.SqlString.Replace("<>", "=", index, 2);
                }

                //>、<
                if (subString.Contains(" > "))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" > ");
                    if (index != -1) sqlCreator.SqlString.Replace(" > ", " <= ", index, 3);
                }
                if (subString.Contains(" < "))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" < ");
                    if (index != -1) sqlCreator.SqlString.Replace(" < ", " >= ", index, 3);
                }

                //>=、<=
                if (subString.Contains(" >= "))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" >= ");
                    if (index != -1) sqlCreator.SqlString.Replace(" >= ", " < ", index, 4);
                }
                if (subString.Contains(" <= "))
                {
                    var index = sqlCreator.ToString().LastIndexOf(" <= ");
                    if (index != -1) sqlCreator.SqlString.Replace(" <= ", " > ", index, 4);
                }
            }
            return sqlCreator;
        }

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, UnaryExpression expression)
        {
            sqlCreator.GroupBy(expression.Operand);
            return sqlCreator;
        }

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, UnaryExpression expression, params OrderType[] orders)
        {
            sqlCreator.OrderBy(expression.Operand, orders);
            return sqlCreator;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Join(SqlCreator sqlCreator, UnaryExpression expression)
        {
            sqlCreator.Join(expression.Operand);
            return sqlCreator;
        }
        #endregion
    }
}