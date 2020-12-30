using System;
using VNext.Entity;

namespace VNext.Entity
{
    public static class QueryForExtensions
    {
        public static SqlBuilder ForFirebird(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.Firebird, fn);
        }

        public static SqlBuilder ForMySql(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.MySql, fn);
        }

        public static SqlBuilder ForOracle(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.Oracle, fn);
        }

        public static SqlBuilder ForPostgreSql(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.PostgreSql, fn);
        }

        public static SqlBuilder ForSqlite(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.Sqlite, fn);
        }

        public static SqlBuilder ForSqlServer(this SqlBuilder src, Func<SqlBuilder, SqlBuilder> fn)
        {
            return src.For(EngineCodes.SqlServer, fn);
        }

    }
}