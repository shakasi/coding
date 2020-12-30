using System;
using System.Data;
using System.Data.Common;
using VNext.Data;
using VNext.Extensions;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using System.Linq;

namespace VNext.Entity.Oracle
{
    /// <summary>
    /// Oracle 的Dapper-Sql功能执行
    /// </summary>
    public class OracleDapperSqlExecutor : SqlExecutorBase
    {
        /// <summary>
        /// 初始化一个<see cref="OracleDapperSqlExecutor"/>类型的新实例
        /// </summary>
        public OracleDapperSqlExecutor(IServiceProvider serviceProvider, string connectionString = "")
            : base(serviceProvider, connectionString)
        { }

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public override DatabaseType DatabaseType => DatabaseType.Oracle;

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected override IDbConnection GetDbConnection(string connectionString)
        {
            Check.NotNullOrEmpty(connectionString, nameof(connectionString));
            DbConnection connection = null;

            var conArray = connectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var providerStr = conArray?.FirstOrDefault(a => a.Contains("ProviderName"));
            var providerKeyValue = providerStr
                    ?.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            var provider = string.Empty;
            if (providerKeyValue != null && providerKeyValue.Length == 2)
            {
                provider = providerKeyValue[1];
                conArray = conArray.ToList().Where(a => a != providerStr).ToArray();
                connectionString = string.Join(";", conArray);
            }

            switch (provider)
            {
                case "OleDb":
                    connection = new System.Data.OleDb.OleDbConnection(connectionString);
                    break;
                default:
                    connection = new OracleConnection(connectionString);
                    break;
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }
    }
}
