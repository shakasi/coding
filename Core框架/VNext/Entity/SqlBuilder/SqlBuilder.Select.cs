using System;
using System.Linq;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {

        public SqlBuilder Select(params string[] columns)
        {
            Method = "select";

            columns = columns
                .Select(x => Helper.ExpandExpression(x))
                .SelectMany(x => x)
                .ToArray();


            foreach (var column in columns)
            {
                AddComponent("select", new Column
                {
                    Name = column
                });
            }

            return this;
        }

        /// <summary>
        /// Add a new "raw" select expression to the query.
        /// </summary>
        /// <returns></returns>
        public SqlBuilder SelectRaw(string sql, params object[] bindings)
        {
            Method = "select";

            AddComponent("select", new RawColumn
            {
                Expression = sql,
                Bindings = bindings,
            });

            return this;
        }

        public SqlBuilder Select(SqlBuilder query, string alias)
        {
            Method = "select";

            query = query.Clone();

            AddComponent("select", new QueryColumn
            {
                Query = query.As(alias),
            });

            return this;
        }

        public SqlBuilder Select(Func<SqlBuilder, SqlBuilder> callback, string alias)
        {
            return Select(callback.Invoke(NewChild()), alias);
        }
    }
}