using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示将委托或lambda表达式应用于参数表达式列表的表达式
    /// </summary>
    public class InvocationExpressionProvider : ExpressionToSqlBase<InvocationExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <returns>SqlBuildPack</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, InvocationExpression expression)
        {
            sqlCreator.Where(expression.Expression);
            return sqlCreator;
        }
        #endregion
    }
}
