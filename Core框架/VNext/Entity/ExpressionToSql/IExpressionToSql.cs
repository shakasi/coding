using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// IExpressionToSql
    /// </summary>
	public interface IExpressionToSql
    {
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sql创建器</returns>
        SqlCreator Select(SqlCreator sqlCreator, Expression expression);

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sql创建器</returns>
        SqlCreator Join(SqlCreator sqlCreator, Expression expression);

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sql创建器</returns>
        SqlCreator Where(SqlCreator sqlCreator, Expression expression);

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sql创建器</returns>
        SqlCreator In(SqlCreator sqlCreator, Expression expression);

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sql创建器</returns>
        SqlCreator GroupBy(SqlCreator sqlCreator, Expression expression);

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql创建器</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序字段</param>
        /// <returns>sql创建器</returns>
        SqlCreator OrderBy(SqlCreator sqlCreator, Expression expression, params OrderType[] orders);
    }
}
