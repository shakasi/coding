using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 描述一个lambda表达式
    /// </summary>
    public class LambdaExpressionProvider : ExpressionToSqlBase<LambdaExpression>
    {
        #region Override Base Class Methods
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, LambdaExpression expression)
        {
            sqlCreator.Where(expression.Body);
            return sqlCreator;
        }
        #endregion
    }
}
