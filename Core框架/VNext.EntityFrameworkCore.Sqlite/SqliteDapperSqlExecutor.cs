using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.Common;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Entity.Sqlite
{
    /// <summary>
    /// Sqlite的Dapper-Sql功能执行
    /// </summary>
    public class SqliteDapperSqlExecutor : SqlExecutorBase
    {
        /// <summary>
        /// 初始化一个<see cref="SqliteDapperSqlExecutor"/>类型的新实例
        /// </summary>
        public SqliteDapperSqlExecutor(IServiceProvider serviceProvider, string connectionString = "")
            : base(serviceProvider, connectionString)
        { }

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public override DatabaseType DatabaseType => DatabaseType.Sqlite;

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected override IDbConnection GetDbConnection(string connectionString)
        {
            Check.NotNullOrEmpty(connectionString, nameof(connectionString));
            DbConnection connection = new SqliteConnection(connectionString); ;
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }
}