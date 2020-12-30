using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {
        public SqlBuilder AsUpdate(object data)
        {
            var dictionary = BuildKeyValuePairsFromObject(data, considerKeys: true);

            return AsUpdate(dictionary);
        }

        public SqlBuilder AsUpdate(IEnumerable<string> columns, IEnumerable<object> values)
        {
            if ((columns?.Any() ?? false) == false || (values?.Any() ?? false) == false)
            {
                throw new InvalidOperationException($"{columns} and {values} cannot be null or empty");
            }

            if (columns.Count() != values.Count())
            {
                throw new InvalidOperationException($"{columns} count should be equal to {values} count");
            }

            Method = "update";

            ClearComponent("update").AddComponent("update", new InsertClause
            {
                Columns = columns.ToList(),
                Values = values.ToList()
            });

            return this;
        }

        public SqlBuilder AsUpdate(IEnumerable<KeyValuePair<string, object>> values)
        {
            if (values == null || values.Any() == false)
            {
                throw new InvalidOperationException($"{values} cannot be null or empty");
            }

            Method = "update";

            ClearComponent("update").AddComponent("update", new InsertClause
            {
                Columns = values.Select(x => x.Key).ToList(),
                Values = values.Select(x => x.Value).ToList(),
            });

            return this;
        }
    }
}