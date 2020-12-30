using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Entity
{
    public static class CompilerExtension
    {
        /// <summary>
        /// 根据数据库类型获取 对应Compiler解析器
        /// </summary>
        public static Compiler GetCompiler(this DatabaseType @this)
        {
            Compiler compiler = null;
            if (@this == DatabaseType.SqlServer)
            {
                compiler = CompilersContainer.Get<SqlServerCompiler>(EngineCodes.SqlServer);
            }
            else if (@this == DatabaseType.Sqlite)
            {
                compiler = CompilersContainer.Get<SqliteCompiler>(EngineCodes.Sqlite);
            }
            else if (@this == DatabaseType.MySql)
            {
                compiler = CompilersContainer.Get<MySqlCompiler>(EngineCodes.MySql);
            }
            else if (@this == DatabaseType.Oracle)
            {
                compiler = CompilersContainer.Get<OracleCompiler>(EngineCodes.Oracle);
            }
            else if (@this == DatabaseType.PostgreSql)
            {
                compiler = CompilersContainer.Get<PostgresCompiler>(EngineCodes.PostgreSql);
            }
            return compiler;
        }

        /// <summary>
        /// 根据SQL语句执行器，获取对应数据库的Compiler
        /// </summary>
        public static Compiler GetCompiler(this ISqlExecutor @this)
        {
            return @this.DatabaseType.GetCompiler();
        }
    }
}
