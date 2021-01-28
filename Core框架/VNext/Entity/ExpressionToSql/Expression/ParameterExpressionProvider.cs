using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示命名参数表达式
    /// </summary>
    public class ParameterExpressionProvider : ExpressionToSqlBase<ParameterExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Select(SqlCreator sqlCreator, ParameterExpression expression)
        {
            string tableAlias = sqlCreator.GetEntityInfo(expression.Type).tableAliasName;
            sqlCreator.SelectFields.Add($"{tableAlias}.*");
            return sqlCreator;
        }
        #endregion
    }
}
