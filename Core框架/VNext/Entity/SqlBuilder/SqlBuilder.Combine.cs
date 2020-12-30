using System;
using System.Linq;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {

        public SqlBuilder Combine(string operation, bool all, SqlBuilder query)
        {
            if (this.Method != "select" || query.Method != "select")
            {
                throw new InvalidOperationException("Only select queries can be combined.");
            }

            return AddComponent("combine", new Combine
            {
                Query = query,
                Operation = operation,
                All = all,
            });
        }

        public SqlBuilder CombineRaw(string sql, params object[] bindings)
        {
            if (this.Method != "select")
            {
                throw new InvalidOperationException("Only select queries can be combined.");
            }

            return AddComponent("combine", new RawCombine
            {
                Expression = sql,
                Bindings = bindings,
            });
        }

        public SqlBuilder Union(SqlBuilder query, bool all = false)
        {
            return Combine("union", all, query);
        }

        public SqlBuilder UnionAll(SqlBuilder query)
        {
            return Union(query, true);
        }

        public SqlBuilder Union(Func<SqlBuilder, SqlBuilder> callback, bool all = false)
        {
            var query = callback.Invoke(new SqlBuilder());
            return Union(query, all);
        }

        public SqlBuilder UnionAll(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Union(callback, true);
        }

        public SqlBuilder UnionRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

        public SqlBuilder Except(SqlBuilder query, bool all = false)
        {
            return Combine("except", all, query);
        }

        public SqlBuilder ExceptAll(SqlBuilder query)
        {
            return Except(query, true);
        }

        public SqlBuilder Except(Func<SqlBuilder, SqlBuilder> callback, bool all = false)
        {
            var query = callback.Invoke(new SqlBuilder());
            return Except(query, all);
        }

        public SqlBuilder ExceptAll(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Except(callback, true);
        }
        public SqlBuilder ExceptRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

        public SqlBuilder Intersect(SqlBuilder query, bool all = false)
        {
            return Combine("intersect", all, query);
        }

        public SqlBuilder IntersectAll(SqlBuilder query)
        {
            return Intersect(query, true);
        }

        public SqlBuilder Intersect(Func<SqlBuilder, SqlBuilder> callback, bool all = false)
        {
            var query = callback.Invoke(new SqlBuilder());
            return Intersect(query, all);
        }

        public SqlBuilder IntersectAll(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Intersect(callback, true);
        }
        public SqlBuilder IntersectRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

    }
}