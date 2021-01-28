using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Entity
{
    public class CompilersContainer
    {
        public const string ERR_INVALID_ENGINECODE = "Engine code '{0}' is not valid";

        protected readonly static IDictionary<string, Compiler> Compilers = new Dictionary<string, Compiler>
        {
            [EngineCodes.Firebird] = new FirebirdCompiler(),
            [EngineCodes.MySql] = new MySqlCompiler(),
            [EngineCodes.Oracle] = new OracleCompiler(),
            [EngineCodes.PostgreSql] = new PostgresCompiler(),
            [EngineCodes.Sqlite] = new SqliteCompiler(),
            [EngineCodes.SqlServer] = new SqlServerCompiler()
        };

        /// <summary>
        /// Returns a <see cref="Compiler"/> instance for the given engine code
        /// </summary>
        /// <param name="engineCode"></param>
        /// <returns></returns>
        public static Compiler Get(string engineCode)
        {
            if (!Compilers.ContainsKey(engineCode))
            {
                throw new InvalidOperationException(string.Format(ERR_INVALID_ENGINECODE, engineCode));
            }

            return Compilers[engineCode];
        }

        /// <summary>
        /// Convenience method <seealso cref="Get"/>
        /// </summary>
        /// <remarks>Does not validate generic type against engine code before cast</remarks>
        /// <typeparam name="TCompiler"></typeparam>
        /// <param name="engineCode"></param>
        /// <returns></returns>
        public static TCompiler Get<TCompiler>(string engineCode) where TCompiler : Compiler
        {
            return (TCompiler)Get(engineCode);
        }
    }
}
