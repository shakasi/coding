using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VNext.Extensions;
using VNext.Systems;

namespace VNext.Entity
{
    /// <summary>
    /// SqlBuilder功能执行基类
    /// </summary>
    public abstract class SqlExecutorBase : Disposable, ISqlExecutor
    {
        private readonly IServiceProvider serviceProvider;
        private readonly string connectionString;

        /// <summary>
        /// 超时时长，默认20s
        /// </summary>
        public int CommandTimeout { get; set; } = 20;

        /// <summary>
        /// 初始化一个<see cref="SqlExecutorBase"/>类型的新实例
        /// </summary>
        protected SqlExecutorBase(IServiceProvider serviceProvider, string connectionString)
        {
            this.serviceProvider = serviceProvider;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected abstract IDbConnection GetDbConnection(string connectionString);      

        #region FromSql
        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        public virtual IEnumerable<TResult> FromSql<TResult>(string sql, object param = null) where TResult : class, IEntityDapper
        {
            using (IDbConnection db = GetDbConnection(connectionString))
            {
                return db.Query<TResult>(sql, param, commandTimeout: CommandTimeout);
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        public virtual IEnumerable<TResult> FromSql<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return connection.Query<TResult>(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
            }
        }


        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        public virtual async Task<IEnumerable<TResult>> FromSqlAsync<TResult>(string sql, object param = null) where TResult : class, IEntityDapper
        {
            using (IDbConnection db = GetDbConnection(connectionString))
            {
                return await db.QueryAsync<TResult>(sql, param, commandTimeout: CommandTimeout);
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        public virtual async Task<IEnumerable<TResult>> FromSqlAsync<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return await connection.QueryAsync<TResult>(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
            }
        }
        #endregion

        #region FromEntity
        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        public virtual TResult FromEntity<TResult>(string sql, object param = null) where TResult : class, IEntityDapper
        {
            using (IDbConnection db = GetDbConnection(connectionString))
            {
                return db.QueryFirstOrDefault<TResult>(sql, param, commandTimeout: CommandTimeout);
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        public virtual TResult FromEntity<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<TResult>(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
            }
        }

        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        public virtual async Task<TResult> FromEntityAsync<TResult>(string sql, object param = null) where TResult : class, IEntityDapper
        {
            using (IDbConnection db = GetDbConnection(connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<TResult>(sql, param, commandTimeout: CommandTimeout);
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回集合</returns>
        public virtual async Task<TResult> FromEntityAsync<TResult>(string sql, params DbParameter[] dbParameter) where TResult : class, IEntityDapper
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<TResult>(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
            }
        }
        #endregion

        #region FromStoredProcedure
        /// <summary>
        /// 执行sql存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回受影响行数</returns>
        public virtual IEnumerable<TResult> FromStoredProcedure<TResult>(string procName, object parameter) where TResult : class, IEntityDapper
        {
            IEnumerable<TResult> result = null;
            using (var connection = GetDbConnection(connectionString))
            {
                result = connection.Query<TResult>(procName, parameter, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
            }
            return result;
        }

        /// <summary>
        /// 执行sql存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回受影响行数</returns>
        public virtual async Task<IEnumerable<TResult>> FromStoredProcedureAsync<TResult>(string procName, object parameter) where TResult : class, IEntityDapper
        {
            IEnumerable<TResult> result = null;
            using (var connection = GetDbConnection(connectionString))
            {
                result = await connection.QueryAsync<TResult>(procName, parameter, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
            }
            return result;
        }
        #endregion

        #region FromTable
        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        public DataTable FromTable(string sql, object parameter)
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return connection.ExecuteReader(sql, parameter, commandTimeout: CommandTimeout).ToDataTable();
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        public DataTable FromTable(string sql, params DbParameter[] dbParameter)
        {
            using (var connection = GetDbConnection(connectionString))
            {
                return connection.ExecuteReader(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout).ToDataTable();
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        public async Task<DataTable> FromTableAsync(string sql, object parameter)
        {
            using (var connection = GetDbConnection(connectionString))
            {
                var reader = await connection.ExecuteReaderAsync(sql, parameter, commandTimeout: CommandTimeout);
                return reader.ToDataTable();
            }
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回DataTable</returns>
        public async Task<DataTable> FromTableAsync(string sql, params DbParameter[] dbParameter)
        {
            using (var connection = GetDbConnection(connectionString))
            {
                var reader = await connection.ExecuteReaderAsync(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
                return reader.ToDataTable();
            }
        }
        #endregion

        #region FromMultiple
        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        public List<IEnumerable<dynamic>> FromMultiple(string sql, object parameter)
        {
            var list = new List<IEnumerable<dynamic>>();
            using (var connection = GetDbConnection(connectionString))
            {
                var result = connection.QueryMultiple(sql, parameter, commandTimeout: CommandTimeout);
                while (result?.IsConsumed == false)
                {
                    list.Add(result.Read());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        public List<IEnumerable<dynamic>> FromMultiple(string sql, params DbParameter[] dbParameter)
        {
            var list = new List<IEnumerable<dynamic>>();
            using (var connection = GetDbConnection(connectionString))
            {
                var result = connection.QueryMultiple(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
                while (result?.IsConsumed == false)
                {
                    list.Add(result.Read());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        public async Task<List<IEnumerable<dynamic>>> FromMultipleAsync(string sql, object parameter)
        {
            var list = new List<IEnumerable<dynamic>>();
            using (var connection = GetDbConnection(connectionString))
            {
                var result = await connection.QueryMultipleAsync(sql, parameter, commandTimeout: CommandTimeout);
                while (result?.IsConsumed == false)
                {
                    list.Add(await result.ReadAsync());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据sql语句查询返回多个结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">对应参数</param>
        /// <returns>返回查询结果集</returns>
        public async Task<List<IEnumerable<dynamic>>> FromMultipleAsync(string sql, params DbParameter[] dbParameter)
        {
            var list = new List<IEnumerable<dynamic>>();
            using (var connection = GetDbConnection(connectionString))
            {
                var result = await connection.QueryMultipleAsync(sql, dbParameter.ToDynamicParameters(), commandTimeout: CommandTimeout);
                while (result?.IsConsumed == false)
                {
                    list.Add(await result.ReadAsync());
                }
            }
            return list;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                //this.Compiler = null;
            }

            base.Dispose(disposing);
        }

        #region Cancel
        ///// <summary>
        ///// Sql语句解析器
        ///// </summary>
        //protected Compiler Compiler
        //{
        //    get { return DatabaseType.GetCompiler(); }
        //    set { Compiler = value; }
        //}

        //public IEnumerable<TResult> Get<TResult>(SqlBuilder sqlBuilder) where TResult : class, IEntityDapper
        //{
        //    var sqlResult = Compiler.Compile(sqlBuilder);

        //    return FromSql<TResult>(sqlResult.Sql, sqlResult.NamedBindings);
        //    IEnumerable<TResult> result = null;
        //    using (IDbConnection db = GetDbConnection(connectionString))
        //    {
        //        var compiled = Compiler.Compile(sqlBuilder);
        //        result = db.Query<TResult>(
        //         compiled.Sql,
        //         compiled.NamedBindings,
        //         commandTimeout: CommandTimeout).ToList();
        //    }
        //    return result;
        //}
        #endregion
    }
}