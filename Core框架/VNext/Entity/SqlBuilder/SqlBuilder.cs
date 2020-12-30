using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNext.Entity
{
    public partial class SqlBuilder : BaseQuery<SqlBuilder>
    {
        public bool IsDistinct { get; set; } = false;
        public string QueryAlias { get; set; }
        public string Method { get; set; } = "select";
        public string QueryComment { get; set; }
        public List<Include> Includes = new List<Include>();
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        public SqlBuilder() : base()
        {
        }

        public SqlBuilder(string table, string comment = null) : base()
        {
            From(table);
            Comment(comment);
        }

        public bool HasOffset(string engineCode = null) => GetOffset(engineCode) > 0;

        public bool HasLimit(string engineCode = null) => GetLimit(engineCode) > 0;

        internal int GetOffset(string engineCode = null)
        {
            engineCode = engineCode ?? EngineScope;
            var offset = this.GetOneComponent<OffsetClause>("offset", engineCode);

            return offset?.Offset ?? 0;
        }

        internal int GetLimit(string engineCode = null)
        {
            engineCode = engineCode ?? EngineScope;
            var limit = this.GetOneComponent<LimitClause>("limit", engineCode);

            return limit?.Limit ?? 0;
        }

        public override SqlBuilder Clone()
        {
            var clone = base.Clone();
            clone.Parent = Parent;
            clone.QueryAlias = QueryAlias;
            clone.IsDistinct = IsDistinct;
            clone.Method = Method;
            clone.Includes = Includes;
            clone.Variables = Variables;
            return clone;
        }

        public SqlBuilder As(string alias)
        {
            QueryAlias = alias;
            return this;
        }

        public SqlBuilder Comment(string comment)
        {
            QueryComment = comment;
            return this;
        }

        public SqlBuilder For(string engine, Func<SqlBuilder, SqlBuilder> fn)
        {
            EngineScope = engine;

            var result = fn.Invoke(this);

            // reset the engine
            EngineScope = null;

            return result;
        }

        public SqlBuilder With(SqlBuilder query)
        {
            // Clear query alias and add it to the containing clause
            if (string.IsNullOrWhiteSpace(query.QueryAlias))
            {
                throw new InvalidOperationException("No Alias found for the CTE query");
            }

            query = query.Clone();

            var alias = query.QueryAlias.Trim();

            // clear the query alias
            query.QueryAlias = null;

            return AddComponent("cte", new QueryFromClause
            {
                Query = query,
                Alias = alias,
            });
        }

        public SqlBuilder With(Func<SqlBuilder, SqlBuilder> fn)
        {
            return With(fn.Invoke(new SqlBuilder()));
        }

        public SqlBuilder With(string alias, SqlBuilder query)
        {
            return With(query.As(alias));
        }

        public SqlBuilder With(string alias, Func<SqlBuilder, SqlBuilder> fn)
        {
            return With(alias, fn.Invoke(new SqlBuilder()));
        }

        public SqlBuilder WithRaw(string alias, string sql, params object[] bindings)
        {
            return AddComponent("cte", new RawFromClause
            {
                Alias = alias,
                Expression = sql,
                Bindings = bindings,
            });
        }

        public SqlBuilder Limit(int value)
        {
            var newClause = new LimitClause
            {
                Limit = value
            };

            return AddOrReplaceComponent("limit", newClause);
        }

        public SqlBuilder Offset(int value)
        {
            var newClause = new OffsetClause
            {
                Offset = value
            };

            return AddOrReplaceComponent("offset", newClause);
        }

        /// <summary>
        /// Alias for Limit
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public SqlBuilder Take(int limit)
        {
            return Limit(limit);
        }

        /// <summary>
        /// Alias for Offset
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public SqlBuilder Skip(int offset)
        {
            return Offset(offset);
        }

        /// <summary>
        /// Set the limit and offset for a given page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public SqlBuilder ForPage(int page, int perPage = 15)
        {
            return Skip((page - 1) * perPage).Take(perPage);
        }

        public SqlBuilder Distinct()
        {
            IsDistinct = true;
            return this;
        }

        /// <summary>
        /// Apply the callback's query changes if the given "condition" is true.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whenTrue">Invoked when the condition is true</param>
        /// <param name="whenFalse">Optional, invoked when the condition is false</param>
        /// <returns></returns>
        public SqlBuilder When(bool condition, Func<SqlBuilder, SqlBuilder> whenTrue, Func<SqlBuilder, SqlBuilder> whenFalse = null)
        {
            if (condition && whenTrue != null)
            {
                return whenTrue.Invoke(this);
            }

            if (!condition && whenFalse != null)
            {
                return whenFalse.Invoke(this);
            }

            return this;
        }

        /// <summary>
        /// Apply the callback's query changes if the given "condition" is false.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SqlBuilder WhenNot(bool condition, Func<SqlBuilder, SqlBuilder> callback)
        {
            if (!condition)
            {
                return callback.Invoke(this);
            }

            return this;
        }

        public SqlBuilder OrderBy(params string[] columns)
        {
            foreach (var column in columns)
            {
                AddComponent("order", new OrderBy
                {
                    Column = column,
                    Ascending = true
                });
            }

            return this;
        }

        public SqlBuilder OrderByDesc(params string[] columns)
        {
            foreach (var column in columns)
            {
                AddComponent("order", new OrderBy
                {
                    Column = column,
                    Ascending = false
                });
            }

            return this;
        }

        public SqlBuilder OrderByRaw(string expression, params object[] bindings)
        {
            return AddComponent("order", new RawOrderBy
            {
                Expression = expression,
                Bindings = Helper.Flatten(bindings).ToArray()
            });
        }

        public SqlBuilder OrderByRandom(string seed)
        {
            return AddComponent("order", new OrderByRandom { });
        }

        public SqlBuilder GroupBy(params string[] columns)
        {
            foreach (var column in columns)
            {
                AddComponent("group", new Column
                {
                    Name = column
                });
            }

            return this;
        }

        public SqlBuilder GroupByRaw(string expression, params object[] bindings)
        {
            AddComponent("group", new RawColumn
            {
                Expression = expression,
                Bindings = bindings,
            });

            return this;
        }

        public override SqlBuilder NewQuery()
        {
            return new SqlBuilder();
        }

        public SqlBuilder Include(string relationName, SqlBuilder query, string foreignKey = null, string localKey = "Id", bool isMany = false)
        {

            Includes.Add(new Include
            {
                Name = relationName,
                LocalKey = localKey,
                ForeignKey = foreignKey,
                Query = query,
                IsMany = isMany,
            });

            return this;
        }

        public SqlBuilder IncludeMany(string relationName, SqlBuilder query, string foreignKey = null, string localKey = "Id")
        {
            return Include(relationName, query, foreignKey, localKey, isMany: true);
        }

        /// <summary>
        /// Define a variable to be used within the query
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlBuilder Define(string variable, object value)
        {
            Variables.Add(variable, value);

            return this;
        }

        public object FindVariable(string variable)
        {
            var found = Variables.ContainsKey(variable);

            if (found)
            {
                return Variables[variable];
            }

            if (Parent != null)
            {
                return (Parent as SqlBuilder).FindVariable(variable);
            }

            throw new Exception($"Variable '{variable}' not found");
        }

        /// <summary>
        /// Gather a list of key-values representing the properties of the object and their values.
        /// </summary>
        /// <param name="data">The plain C# object</param>
        /// <param name="considerKeys">
        /// When true it will search for properties with the [Key] attribute
        /// and will add it automatically to the Where clause
        /// </param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, object>> BuildKeyValuePairsFromObject(object data, bool considerKeys = false)
        {
            var dictionary = new Dictionary<string, object>();
            var props = data.GetType().GetRuntimeProperties();

            foreach (var property in props)
            {
                if (property.GetCustomAttribute(typeof(IgnoreAttribute)) != null)
                {
                    continue;
                }

                var value = property.GetValue(data);

                var colAttr = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;

                var name = colAttr?.Name ?? property.Name;

                dictionary.Add(name, value);

                if (considerKeys && colAttr != null)
                {
                    if ((colAttr as KeyAttribute) != null)
                    {
                        this.Where(name, value);
                    }
                }
            }

            return dictionary;
        }
    }
}
