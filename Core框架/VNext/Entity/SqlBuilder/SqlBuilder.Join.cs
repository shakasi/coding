using System;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {

        private SqlBuilder Join(Func<Join, Join> callback)
        {
            var join = callback.Invoke(new Join().AsInner());

            return AddComponent("join", new BaseJoin
            {
                Join = join
            });
        }

        public SqlBuilder Join(
            string table,
            string first,
            string second,
            string op = "=",
            string type = "inner join"
        )
        {
            return Join(j => j.JoinWith(table).WhereColumns(first, op, second).AsType(type));
        }

        public SqlBuilder Join(string table, Func<Join, Join> callback, string type = "inner join")
        {
            return Join(j => j.JoinWith(table).Where(callback).AsType(type));
        }

        public SqlBuilder Join(SqlBuilder query, Func<Join, Join> onCallback, string type = "inner join")
        {
            return Join(j => j.JoinWith(query).Where(onCallback).AsType(type));
        }

        public SqlBuilder LeftJoin(string table, string first, string second, string op = "=")
        {
            return Join(table, first, second, op, "left join");
        }

        public SqlBuilder LeftJoin(string table, Func<Join, Join> callback)
        {
            return Join(table, callback, "left join");
        }

        public SqlBuilder LeftJoin(SqlBuilder query, Func<Join, Join> onCallback)
        {
            return Join(query, onCallback, "left join");
        }

        public SqlBuilder RightJoin(string table, string first, string second, string op = "=")
        {
            return Join(table, first, second, op, "right join");
        }

        public SqlBuilder RightJoin(string table, Func<Join, Join> callback)
        {
            return Join(table, callback, "right join");
        }

        public SqlBuilder RightJoin(SqlBuilder query, Func<Join, Join> onCallback)
        {
            return Join(query, onCallback, "right join");
        }

        public SqlBuilder CrossJoin(string table)
        {
            return Join(j => j.JoinWith(table).AsCross());
        }

    }
}