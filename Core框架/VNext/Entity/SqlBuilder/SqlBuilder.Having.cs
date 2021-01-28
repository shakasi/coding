using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNext.Entity
{
    public partial class SqlBuilder
    {
        public SqlBuilder Having(string column, string op, object value)
        {

            // If the value is "null", we will just assume the developer wants to add a
            // Having null clause to the query. So, we will allow a short-cut here to
            // that method for convenience so the developer doesn't have to check.
            if (value == null)
            {
                return Not(op != "=").HavingNull(column);
            }

            return AddComponent("having", new BasicCondition
            {
                Column = column,
                Operator = op,
                Value = value,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNot(string column, string op, object value)
        {
            return Not().Having(column, op, value);
        }

        public SqlBuilder OrHaving(string column, string op, object value)
        {
            return Or().Having(column, op, value);
        }

        public SqlBuilder OrHavingNot(string column, string op, object value)
        {
            return this.Or().Not().Having(column, op, value);
        }

        public SqlBuilder Having(string column, object value)
        {
            return Having(column, "=", value);
        }
        public SqlBuilder HavingNot(string column, object value)
        {
            return HavingNot(column, "=", value);
        }
        public SqlBuilder OrHaving(string column, object value)
        {
            return OrHaving(column, "=", value);
        }
        public SqlBuilder OrHavingNot(string column, object value)
        {
            return OrHavingNot(column, "=", value);
        }

        /// <summary>
        /// Perform a Having constraint
        /// </summary>
        /// <param name="constraints"></param>
        /// <returns></returns>
        public SqlBuilder Having(object constraints)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var item in constraints.GetType().GetRuntimeProperties())
            {
                dictionary.Add(item.Name, item.GetValue(constraints));
            }

            return Having(dictionary);
        }

        public SqlBuilder Having(IEnumerable<KeyValuePair<string, object>> values)
        {
            var query = this;
            var orFlag = GetOr();
            var notFlag = GetNot();

            foreach (var tuple in values)
            {
                if (orFlag)
                {
                    query = query.Or();
                }
                else
                {
                    query.And();
                }

                query = this.Not(notFlag).Having(tuple.Key, tuple.Value);
            }

            return query;
        }

        public SqlBuilder HavingRaw(string sql, params object[] bindings)
        {
            return AddComponent("having", new RawCondition
            {
                Expression = sql,
                Bindings = bindings,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder OrHavingRaw(string sql, params object[] bindings)
        {
            return Or().HavingRaw(sql, bindings);
        }

        /// <summary>
        /// Apply a nested Having clause
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SqlBuilder Having(Func<SqlBuilder, SqlBuilder> callback)
        {
            var query = callback.Invoke(NewChild());

            return AddComponent("having", new NestedCondition<SqlBuilder>
            {
                Query = query,
                IsNot = GetNot(),
                IsOr = GetOr(),
            });
        }

        public SqlBuilder HavingNot(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Not().Having(callback);
        }

        public SqlBuilder OrHaving(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().Having(callback);
        }

        public SqlBuilder OrHavingNot(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Not().Or().Having(callback);
        }

        public SqlBuilder HavingColumns(string first, string op, string second)
        {
            return AddComponent("having", new TwoColumnsCondition
            {
                First = first,
                Second = second,
                Operator = op,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder OrHavingColumns(string first, string op, string second)
        {
            return Or().HavingColumns(first, op, second);
        }

        public SqlBuilder HavingNull(string column)
        {
            return AddComponent("having", new NullCondition
            {
                Column = column,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNotNull(string column)
        {
            return Not().HavingNull(column);
        }

        public SqlBuilder OrHavingNull(string column)
        {
            return this.Or().HavingNull(column);
        }

        public SqlBuilder OrHavingNotNull(string column)
        {
            return Or().Not().HavingNull(column);
        }

        public SqlBuilder HavingTrue(string column)
        {
            return AddComponent("having", new BooleanCondition
            {
                Column = column,
                Value = true,
            });
        }

        public SqlBuilder OrHavingTrue(string column)
        {
            return Or().HavingTrue(column);
        }

        public SqlBuilder HavingFalse(string column)
        {
            return AddComponent("having", new BooleanCondition
            {
                Column = column,
                Value = false,
            });
        }

        public SqlBuilder OrHavingFalse(string column)
        {
            return Or().HavingFalse(column);
        }

        public SqlBuilder HavingLike(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return AddComponent("having", new BasicStringCondition
            {
                Operator = "like",
                Column = column,
                Value = value,
                CaseSensitive = caseSensitive,
                EscapeCharacter = escapeCharacter,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNotLike(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Not().HavingLike(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingLike(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().HavingLike(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingNotLike(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().Not().HavingLike(column, value, caseSensitive, escapeCharacter);
        }
        public SqlBuilder HavingStarts(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return AddComponent("having", new BasicStringCondition
            {
                Operator = "starts",
                Column = column,
                Value = value,
                CaseSensitive = caseSensitive,
                EscapeCharacter = escapeCharacter,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNotStarts(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Not().HavingStarts(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingStarts(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().HavingStarts(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingNotStarts(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().Not().HavingStarts(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder HavingEnds(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return AddComponent("having", new BasicStringCondition
            {
                Operator = "ends",
                Column = column,
                Value = value,
                CaseSensitive = caseSensitive,
                EscapeCharacter = escapeCharacter,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNotEnds(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Not().HavingEnds(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingEnds(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().HavingEnds(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingNotEnds(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().Not().HavingEnds(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder HavingContains(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return AddComponent("having", new BasicStringCondition
            {
                Operator = "contains",
                Column = column,
                Value = value,
                CaseSensitive = caseSensitive,
                EscapeCharacter = escapeCharacter,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }

        public SqlBuilder HavingNotContains(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Not().HavingContains(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingContains(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().HavingContains(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder OrHavingNotContains(string column, object value, bool caseSensitive = false, string escapeCharacter = null)
        {
            return Or().Not().HavingContains(column, value, caseSensitive, escapeCharacter);
        }

        public SqlBuilder HavingBetween<T>(string column, T lower, T higher)
        {
            return AddComponent("having", new BetweenCondition<T>
            {
                Column = column,
                IsOr = GetOr(),
                IsNot = GetNot(),
                Lower = lower,
                Higher = higher
            });
        }

        public SqlBuilder OrHavingBetween<T>(string column, T lower, T higher)
        {
            return Or().HavingBetween(column, lower, higher);
        }
        public SqlBuilder HavingNotBetween<T>(string column, T lower, T higher)
        {
            return Not().HavingBetween(column, lower, higher);
        }
        public SqlBuilder OrHavingNotBetween<T>(string column, T lower, T higher)
        {
            return Or().Not().HavingBetween(column, lower, higher);
        }

        public SqlBuilder HavingIn<T>(string column, IEnumerable<T> values)
        {
            // If the developer has passed a string most probably he wants List<string>
            // since string is considered as List<char>
            if (values is string)
            {
                string val = values as string;

                return AddComponent("having", new InCondition<string>
                {
                    Column = column,
                    IsOr = GetOr(),
                    IsNot = GetNot(),
                    Values = new List<string> { val }
                });
            }

            return AddComponent("having", new InCondition<T>
            {
                Column = column,
                IsOr = GetOr(),
                IsNot = GetNot(),
                Values = values.Distinct().ToList()
            });


        }

        public SqlBuilder OrHavingIn<T>(string column, IEnumerable<T> values)
        {
            return Or().HavingIn(column, values);
        }

        public SqlBuilder HavingNotIn<T>(string column, IEnumerable<T> values)
        {
            return Not().HavingIn(column, values);
        }

        public SqlBuilder OrHavingNotIn<T>(string column, IEnumerable<T> values)
        {
            return Or().Not().HavingIn(column, values);
        }


        public SqlBuilder HavingIn(string column, SqlBuilder query)
        {
            return AddComponent("having", new InQueryCondition
            {
                Column = column,
                IsOr = GetOr(),
                IsNot = GetNot(),
                Query = query,
            });
        }
        public SqlBuilder HavingIn(string column, Func<SqlBuilder, SqlBuilder> callback)
        {
            var query = callback.Invoke(new SqlBuilder());

            return HavingIn(column, query);
        }

        public SqlBuilder OrHavingIn(string column, SqlBuilder query)
        {
            return Or().HavingIn(column, query);
        }

        public SqlBuilder OrHavingIn(string column, Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().HavingIn(column, callback);
        }
        public SqlBuilder HavingNotIn(string column, SqlBuilder query)
        {
            return Not().HavingIn(column, query);
        }

        public SqlBuilder HavingNotIn(string column, Func<SqlBuilder, SqlBuilder> callback)
        {
            return Not().HavingIn(column, callback);
        }

        public SqlBuilder OrHavingNotIn(string column, SqlBuilder query)
        {
            return Or().Not().HavingIn(column, query);
        }

        public SqlBuilder OrHavingNotIn(string column, Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().Not().HavingIn(column, callback);
        }


        /// <summary>
        /// Perform a sub query Having clause
        /// </summary>
        /// <param name="column"></param>
        /// <param name="op"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SqlBuilder Having(string column, string op, Func<SqlBuilder, SqlBuilder> callback)
        {
            var query = callback.Invoke(NewChild());

            return Having(column, op, query);
        }

        public SqlBuilder Having(string column, string op, SqlBuilder query)
        {
            return AddComponent("having", new QueryCondition<SqlBuilder>
            {
                Column = column,
                Operator = op,
                Query = query,
                IsNot = GetNot(),
                IsOr = GetOr(),
            });
        }

        public SqlBuilder OrHaving(string column, string op, SqlBuilder query)
        {
            return Or().Having(column, op, query);
        }
        public SqlBuilder OrHaving(string column, string op, Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().Having(column, op, callback);
        }

        public SqlBuilder HavingExists(SqlBuilder query)
        {
            if (!query.HasComponent("from"))
            {
                throw new ArgumentException($"{nameof(FromClause)} cannot be empty if used inside a {nameof(HavingExists)} condition");
            }

            // simplify the query as much as possible
            query = query.Clone().ClearComponent("select")
                .SelectRaw("1")
                .Limit(1);

            return AddComponent("having", new ExistsCondition
            {
                Query = query,
                IsNot = GetNot(),
                IsOr = GetOr(),
            });
        }
        public SqlBuilder HavingExists(Func<SqlBuilder, SqlBuilder> callback)
        {
            var childQuery = new SqlBuilder().SetParent(this);
            return HavingExists(callback.Invoke(childQuery));
        }

        public SqlBuilder HavingNotExists(SqlBuilder query)
        {
            return Not().HavingExists(query);
        }

        public SqlBuilder HavingNotExists(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Not().HavingExists(callback);
        }

        public SqlBuilder OrHavingExists(SqlBuilder query)
        {
            return Or().HavingExists(query);
        }
        public SqlBuilder OrHavingExists(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().HavingExists(callback);
        }
        public SqlBuilder OrHavingNotExists(SqlBuilder query)
        {
            return Or().Not().HavingExists(query);
        }
        public SqlBuilder OrHavingNotExists(Func<SqlBuilder, SqlBuilder> callback)
        {
            return Or().Not().HavingExists(callback);
        }

        #region date
        public SqlBuilder HavingDatePart(string part, string column, string op, object value)
        {
            return AddComponent("having", new BasicDateCondition
            {
                Operator = op,
                Column = column,
                Value = value,
                Part = part,
                IsOr = GetOr(),
                IsNot = GetNot(),
            });
        }
        public SqlBuilder HavingNotDatePart(string part, string column, string op, object value)
        {
            return Not().HavingDatePart(part, column, op, value);
        }

        public SqlBuilder OrHavingDatePart(string part, string column, string op, object value)
        {
            return Or().HavingDatePart(part, column, op, value);
        }

        public SqlBuilder OrHavingNotDatePart(string part, string column, string op, object value)
        {
            return Or().Not().HavingDatePart(part, column, op, value);
        }

        public SqlBuilder HavingDate(string column, string op, object value)
        {
            return HavingDatePart("date", column, op, value);
        }
        public SqlBuilder HavingNotDate(string column, string op, object value)
        {
            return Not().HavingDate(column, op, value);
        }
        public SqlBuilder OrHavingDate(string column, string op, object value)
        {
            return Or().HavingDate(column, op, value);
        }
        public SqlBuilder OrHavingNotDate(string column, string op, object value)
        {
            return Or().Not().HavingDate(column, op, value);
        }

        public SqlBuilder HavingTime(string column, string op, object value)
        {
            return HavingDatePart("time", column, op, value);
        }
        public SqlBuilder HavingNotTime(string column, string op, object value)
        {
            return Not().HavingTime(column, op, value);
        }
        public SqlBuilder OrHavingTime(string column, string op, object value)
        {
            return Or().HavingTime(column, op, value);
        }
        public SqlBuilder OrHavingNotTime(string column, string op, object value)
        {
            return Or().Not().HavingTime(column, op, value);
        }

        public SqlBuilder HavingDatePart(string part, string column, object value)
        {
            return HavingDatePart(part, column, "=", value);
        }
        public SqlBuilder HavingNotDatePart(string part, string column, object value)
        {
            return HavingNotDatePart(part, column, "=", value);
        }

        public SqlBuilder OrHavingDatePart(string part, string column, object value)
        {
            return OrHavingDatePart(part, column, "=", value);
        }

        public SqlBuilder OrHavingNotDatePart(string part, string column, object value)
        {
            return OrHavingNotDatePart(part, column, "=", value);
        }

        public SqlBuilder HavingDate(string column, object value)
        {
            return HavingDate(column, "=", value);
        }
        public SqlBuilder HavingNotDate(string column, object value)
        {
            return HavingNotDate(column, "=", value);
        }
        public SqlBuilder OrHavingDate(string column, object value)
        {
            return OrHavingDate(column, "=", value);
        }
        public SqlBuilder OrHavingNotDate(string column, object value)
        {
            return OrHavingNotDate(column, "=", value);
        }

        public SqlBuilder HavingTime(string column, object value)
        {
            return HavingTime(column, "=", value);
        }
        public SqlBuilder HavingNotTime(string column, object value)
        {
            return HavingNotTime(column, "=", value);
        }
        public SqlBuilder OrHavingTime(string column, object value)
        {
            return OrHavingTime(column, "=", value);
        }
        public SqlBuilder OrHavingNotTime(string column, object value)
        {
            return OrHavingNotTime(column, "=", value);
        }

        #endregion
    }
}