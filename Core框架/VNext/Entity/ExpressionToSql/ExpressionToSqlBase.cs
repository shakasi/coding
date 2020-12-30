using System;
using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 抽象基类
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public abstract class ExpressionToSqlBase<T> : IExpressionToSql where T : Expression
    {
        #region 接口实现
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator Select(SqlCreator sqlCreator, Expression expression) => Select(sqlCreator,(T)expression);

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator Join(SqlCreator sqlCreator, Expression expression) => Join(sqlCreator, (T)expression);

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator Where(SqlCreator sqlCreator,Expression expression) => Where(sqlCreator, (T)expression);

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator In(SqlCreator sqlCreator,Expression expression) => In(sqlCreator, (T)expression);

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator GroupBy(SqlCreator sqlCreator,Expression expression) => GroupBy(sqlCreator, (T)expression);

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>SqlCreator</returns>
        public SqlCreator OrderBy(SqlCreator sqlCreator, Expression expression, params OrderType[] orders) => OrderBy(sqlCreator, (T)expression, orders);
        #endregion

        #region 虚实现方法
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator Select(SqlCreator sqlCreator, T expression)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.Select方法");

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator Join(SqlCreator sqlCreator, T expression)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.Join方法");

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator Where(SqlCreator sqlCreator, T expression)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.Where方法");

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator In(SqlCreator sqlCreator, T expression)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.In方法");

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator GroupBy(SqlCreator sqlCreator, T expression)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.GroupBy方法");

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>SqlCreator</returns>
        public virtual SqlCreator OrderBy(SqlCreator sqlCreator, T expression, params OrderType[] orders)
            => throw new NotImplementedException("未实现" + typeof(T).Name + "IExpressionToSql.OrderBy方法");
        #endregion
    }
}