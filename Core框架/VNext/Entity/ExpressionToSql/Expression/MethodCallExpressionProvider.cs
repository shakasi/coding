using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示对静态方法或实例方法的调用
    /// </summary>
	public class MethodCallExpressionProvider : ExpressionToSqlBase<MethodCallExpression>
    {
        #region Private Static Methods
        /// <summary>
        /// methods
        /// </summary>
        private static readonly Dictionary<string, Action<SqlCreator, MethodCallExpression>> methods = new Dictionary<string, Action<SqlCreator, MethodCallExpression>>
        {
            ["Like"] = Like,
            ["LikeLeft"] = LikeLeft,
            ["LikeRight"] = LikeRight,
            ["NotLike"] = NotLike,
            ["In"] = IN,
            ["NotIn"] = NotIn,
            ["Contains"] = Contains,
            ["IsNullOrEmpty"] = IsNullOrEmpty,
            ["Equals"] = Equals,
            ["ToUpper"] = ToUpper,
            ["ToLower"] = ToLower,
            ["Trim"] = Trim,
            ["TrimStart"] = TrimStart,
            ["TrimEnd"] = TrimEnd
        };

        /// <summary>
        /// IN
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void IN(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            sqlCreator.Where(expression.Arguments[0]);
            sqlCreator += " IN ";
            sqlCreator.In(expression.Arguments[1]);
        }

        /// <summary>
        /// Not In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void NotIn(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            sqlCreator.Where(expression.Arguments[0]);
            sqlCreator += " NOT IN ";
            sqlCreator.In(expression.Arguments[1]);
        }

        /// <summary>
        /// Like
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void Like(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator.Where(expression.Object);
            }
            sqlCreator.Where(expression.Arguments[0]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " LIKE '%' + ";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += " LIKE CONCAT('%',";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " LIKE '%' || ";
                    break;
                default:
                    break;
            }
            sqlCreator.Where(expression.Arguments[1]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " + '%'";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += ",'%')";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " || '%'";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// LikeLeft
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void LikeLeft(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator.Where(expression.Object);
            }
            sqlCreator.Where(expression.Arguments[0]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " LIKE '%' + ";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += " LIKE CONCAT('%',";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " LIKE '%' || ";
                    break;
                default:
                    break;
            }
            sqlCreator.Where(expression.Arguments[1]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += ")";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// LikeRight
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void LikeRight(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator.Where(expression.Object);
            }
            sqlCreator.Where(expression.Arguments[0]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " LIKE ";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += " LIKE CONCAT(";
                    break;
                default:
                    break;
            }
            sqlCreator.Where(expression.Arguments[1]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " + '%'";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += ",'%')";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " || '%'";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// NotLike
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void NotLike(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator.Where(expression.Object);
            }
            sqlCreator.Where(expression.Arguments[0]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " NOT LIKE '%' + ";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += " NOT LIKE CONCAT('%',";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " NOT LIKE '%' || ";
                    break;
                default:
                    break;
            }
            sqlCreator.Where(expression.Arguments[1]);
            switch (sqlCreator.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    sqlCreator += " + '%'";
                    break;
                case DatabaseType.MySql:
                case DatabaseType.PostgreSql:
                    sqlCreator += ",'%')";
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.Sqlite:
                    sqlCreator += " || '%'";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void Contains(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                if (typeof(IList).IsAssignableFrom(expression.Object.Type))
                {
                    sqlCreator.Where(expression.Arguments[0]);
                    sqlCreator += " IN ";
                    sqlCreator.In(expression.Object);
                }
                else
                {
                    sqlCreator.Where(expression.Object);
                    switch (sqlCreator.DatabaseType)
                    {
                        case DatabaseType.SqlServer:
                            sqlCreator += " LIKE '%' + ";
                            break;
                        case DatabaseType.MySql:
                        case DatabaseType.PostgreSql:
                            sqlCreator += " LIKE CONCAT('%',";
                            break;
                        case DatabaseType.Oracle:
                        case DatabaseType.Sqlite:
                            sqlCreator += " LIKE '%' || ";
                            break;
                        default:
                            break;
                    }
                    sqlCreator.Where(expression.Arguments[0]);
                    switch (sqlCreator.DatabaseType)
                    {
                        case DatabaseType.SqlServer:
                            sqlCreator += " + '%'";
                            break;
                        case DatabaseType.MySql:
                        case DatabaseType.PostgreSql:
                            sqlCreator += ",'%')";
                            break;
                        case DatabaseType.Oracle:
                        case DatabaseType.Sqlite:
                            sqlCreator += " || '%'";
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (expression.Arguments.Count > 1 && expression.Arguments[1] is MemberExpression memberExpression)
            {
                sqlCreator.Where(memberExpression);
                sqlCreator += " IN ";
                sqlCreator.In(expression.Arguments[0]);
            }
        }

        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void IsNullOrEmpty(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            sqlCreator += "(";
            sqlCreator.Where(expression.Arguments[0]);
            sqlCreator += " IS NULL OR ";
            sqlCreator.Where(expression.Arguments[0]);
            sqlCreator += " = ''";
            sqlCreator += ")";
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void Equals(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator.Where(expression.Object);
            }
            var signIndex = sqlCreator.Length;
            sqlCreator.Where(expression.Arguments[0]);
            if (sqlCreator.ToString().ToUpper().EndsWith("NULL"))
            {
                sqlCreator.SqlString.Insert(signIndex, " IS ");
            }
            else
            {
                sqlCreator.SqlString.Insert(signIndex, " = ");
            }
        }

        /// <summary>
        /// ToUpper
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void ToUpper(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator += "UPPER(";
                sqlCreator.Where(expression.Object);
                sqlCreator += ")";
            }
        }

        /// <summary>
        /// ToLower
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void ToLower(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator += "LOWER(";
                sqlCreator.Where(expression.Object);
                sqlCreator += ")";
            }
        }

        /// <summary>
        /// Trim
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void Trim(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                if (sqlCreator.DatabaseType == DatabaseType.SqlServer)
                {
                    sqlCreator += "LTRIM(RTRIM(";
                }
                else
                {
                    sqlCreator += "TRIM(";
                }
                sqlCreator.Where(expression.Object);
                if (sqlCreator.DatabaseType == DatabaseType.SqlServer)
                {
                    sqlCreator += "))";
                }
                else
                {
                    sqlCreator += ")";
                }
            }
        }

        /// <summary>
        /// TrimStart
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void TrimStart(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator += "LTRIM(";
                sqlCreator.Where(expression.Object);
                sqlCreator += ")";
            }
        }

        /// <summary>
        /// TrimEnd
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        private static void TrimEnd(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            if (expression.Object != null)
            {
                sqlCreator += "RTRIM(";
                sqlCreator.Where(expression.Object);
                sqlCreator += ")";
            }
        }
        #endregion

        #region Override Base Class Methods
        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator In(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            var val = expression?.ToObject();
            if (val != null)
            {
                sqlCreator += "(";
                if (val.GetType().IsArray || typeof(IList).IsAssignableFrom(val.GetType()))
                {
                    var list = val as IList;
                    if (list?.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            ExpressionToSqlProvider.In(sqlCreator, Expression.Constant(item, item.GetType()));
                            sqlCreator += ",";
                        }
                    }
                }
                else
                {
                    ExpressionToSqlProvider.In(sqlCreator, Expression.Constant(val, val.GetType()));
                }
                if (sqlCreator.SqlString[sqlCreator.SqlString.Length - 1] == ',')
                    sqlCreator.SqlString.Remove(sqlCreator.SqlString.Length - 1, 1);
                sqlCreator += ")";
            }
            return sqlCreator;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            var key = expression.Method;
            if (key.IsGenericMethod)
                key = key.GetGenericMethodDefinition();
            if (methods.TryGetValue(key.Name, out Action<SqlCreator, MethodCallExpression> action))
            {
                action(sqlCreator, expression);
                return sqlCreator;
            }
            throw new NotImplementedException("无法解析方法" + expression.Method);
        }


        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlCreator</returns>
        public override SqlCreator GroupBy(SqlCreator sqlCreator, MethodCallExpression expression)
        {
            var array = (expression.ToObject() as IEnumerable<object>)?.ToList();
            if (array != null)
            {
                for (var i = 0; i < array.Count; i++)
                {
                    sqlCreator.GroupBy(Expression.Constant(array[i], array[i].GetType()));
                }
                sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
            }
            return sqlCreator;
        }

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator OrderBy(SqlCreator sqlCreator, MethodCallExpression expression, params OrderType[] orders)
        {
            var array = (expression.ToObject() as IEnumerable<object>)?.ToList();
            if (array != null)
            {
                for (var i = 0; i < array.Count; i++)
                {
                    sqlCreator.OrderBy(Expression.Constant(array[i], array[i].GetType()));
                    if (i <= orders.Length - 1)
                    {
                        sqlCreator += $" { (orders[i] == OrderType.Descending ? "DESC" : "ASC")},";
                    }
                    else if (!array[i].ToString().ToUpper().Contains("ASC") && !array[i].ToString().ToUpper().Contains("DESC"))
                    {
                        sqlCreator += " ASC,";
                    }
                    else
                    {
                        sqlCreator += ",";
                    }
                }
                sqlCreator.SqlString.Remove(sqlCreator.Length - 1, 1);
            }
            return sqlCreator;
        }
        #endregion
    }
}