using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using VNext.Dependency;
using VNext.Extensions;

namespace VNext.Entity
{
    public static class SqlBuilder
    {
        private static Action<string, object, ILogger> loggerSql = (sql, parms, logger) =>
         {
             var logSql = $"\r\n【SQL】:💩 {sql} 💩\r\n【Parms】:💩 {parms.ToJsonString()} 💩";
             try
             {
                 logger = logger ?? ServiceLocator.Instance.GetLogger(typeof(SqlBuilder));
             }
             catch (Exception)
             {
                 logger = null;
             }
             logger?.LogDebug(logSql);
         };

        #region Select
        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="sqlIntercept">sql拦截委托</param>
        /// <returns>SqlBuilderCore</returns>
        public static SqlBuilderCore<T> Select<T>(
            Expression<Func<T, object>> expression = null,
            DatabaseType databaseType = DatabaseType.SqlServer,
            Action<string, object, ILogger> sqlIntercept = null,
            ILogger logger = null)
            where T : class, IEntityDapper
        {
            sqlIntercept = sqlIntercept ?? loggerSql;
            var builder = new SqlBuilderCore<T>(databaseType, logger, sqlIntercept).Select(expression);
            return builder;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="sqlIntercept">sql拦截委托</param>
        /// <param name="logger">日志对象</param>
        /// <returns>SqlBuilderCore</returns>
        public static SqlBuilderCore<T> Select<T, T2>(
            Expression<Func<T, T2, object>> expression = null,
            DatabaseType databaseType = DatabaseType.SqlServer,
            Action<string, object, ILogger> sqlIntercept = null,
            ILogger logger = null)
            where T : class, IEntityDapper
            where T2 : class, IEntityDapper
        {
            sqlIntercept = sqlIntercept ?? loggerSql;
            var builder = new SqlBuilderCore<T>(databaseType, logger, sqlIntercept).Select(expression);
            return builder;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="sqlIntercept">sql拦截委托</param>
        /// <param name="logger">日志对象</param>
        /// <returns>SqlBuilderCore</returns>
        public static SqlBuilderCore<T> Select<T, T2, T3>(
            Expression<Func<T, T2, T3, object>> expression = null,
            DatabaseType databaseType = DatabaseType.SqlServer,
            Action<string, object, ILogger> sqlIntercept = null,
            ILogger logger = null)
            where T : class, IEntityDapper
            where T2 : class, IEntityDapper
            where T3 : class, IEntityDapper
        {
            sqlIntercept = sqlIntercept ?? loggerSql;
            var builder = new SqlBuilderCore<T>(databaseType, logger, sqlIntercept).Select(expression);
            return builder;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <typeparam name="T4">泛型类型4</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="sqlIntercept">sql拦截委托</param>
        /// <param name="logger">日志对象</param>
        /// <returns>SqlBuilderCore</returns>
        public static SqlBuilderCore<T> Select<T, T2, T3, T4>(
            Expression<Func<T, T2, T3, T4, object>> expression = null,
            DatabaseType databaseType = DatabaseType.SqlServer,
            Action<string, object, ILogger> sqlIntercept = null,
            ILogger logger = null)
            where T : class, IEntityDapper
            where T2 : class, IEntityDapper
            where T3 : class, IEntityDapper
            where T4 : class, IEntityDapper
        {
            sqlIntercept = sqlIntercept ?? loggerSql;
            var builder = new SqlBuilderCore<T>(databaseType, logger, sqlIntercept).Select(expression);
            return builder;
        }
        #endregion
    }
}
