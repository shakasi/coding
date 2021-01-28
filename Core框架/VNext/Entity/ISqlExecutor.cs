using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VNext.Entity;

namespace VNext.Entity
{
    /// <summary>
    /// 定义SQL语句执行功能
    /// </summary>
    public interface ISqlExecutor : IDisposable
    {
        DatabaseType DatabaseType { get; }

        #region FromSql
        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        IEnumerable<TResult> FromSql<TResult>(string sql, object param = null) where TResult : class, IEntityDapper;


        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        IEnumerable<TResult> FromSql<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper;


        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<TResult>> FromSqlAsync<TResult>(string sql, object param = null) where TResult : class, IEntityDapper;

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        Task<IEnumerable<TResult>> FromSqlAsync<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper;
        #endregion

        #region FromEntity
        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        TResult FromEntity<TResult>(string sql, object param = null) where TResult : class, IEntityDapper;


        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        TResult FromEntity<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper;


        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        Task<TResult> FromEntityAsync<TResult>(string sql, object param = null) where TResult : class, IEntityDapper;

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        Task<TResult> FromEntityAsync<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper;
        #endregion

        #region FromStoredProcedure
        /// <summary>
        /// 执行sql存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回受影响行数</returns>
        IEnumerable<TResult> FromStoredProcedure<TResult>(string procName, object parameter) where TResult : class, IEntityDapper;

        /// <summary>
        /// 执行sql存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回受影响行数</returns>
        Task<IEnumerable<TResult>> FromStoredProcedureAsync<TResult>(string procName, object parameter) where TResult : class, IEntityDapper;
        #endregion

        #region FromTable
        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        DataTable FromTable(string sql, object parameter);

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        DataTable FromTable(string sql, params DbParameter[] dbParameter);

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        Task<DataTable> FromTableAsync(string sql, object parameter);

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        Task<DataTable> FromTableAsync(string sql, params DbParameter[] dbParameter);
        #endregion

        #region FromMultiple
        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        List<IEnumerable<dynamic>> FromMultiple(string sql, object parameter);

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        List<IEnumerable<dynamic>> FromMultiple(string sql, params DbParameter[] dbParameter);

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        Task<List<IEnumerable<dynamic>>> FromMultipleAsync(string sql, object parameter);

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        Task<List<IEnumerable<dynamic>>> FromMultipleAsync(string sql, params DbParameter[] dbParameter);
        #endregion
    }
}