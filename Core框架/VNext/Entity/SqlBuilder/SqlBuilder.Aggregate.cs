using System.Collections.Generic;
using System.Linq;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {
        public SqlBuilder AsAggregate(string type, string[] columns = null)
        {

            Method = "aggregate";

            this.ClearComponent("aggregate")
            .AddComponent("aggregate", new AggregateClause
            {
                Type = type,
                Columns = columns?.ToList() ?? new List<string>(),
            });

            return this;
        }

        public SqlBuilder AsCount(string[] columns = null)
        {
            var cols = columns?.ToList() ?? new List<string> { };

            if (!cols.Any())
            {
                cols.Add("*");
            }

            return AsAggregate("count", cols.ToArray());
        }

        public SqlBuilder AsAvg(string column)
        {
            return AsAggregate("avg", new string[] { column });
        }
        public SqlBuilder AsAverage(string column)
        {
            return AsAvg(column);
        }

        public SqlBuilder AsSum(string column)
        {
            return AsAggregate("sum", new[] { column });
        }

        public SqlBuilder AsMax(string column)
        {
            return AsAggregate("max", new[] { column });
        }

        public SqlBuilder AsMin(string column)
        {
            return AsAggregate("min", new[] { column });
        }
    }
}