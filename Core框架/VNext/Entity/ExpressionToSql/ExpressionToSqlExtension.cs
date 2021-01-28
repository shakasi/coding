using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using VNext.Extensions;

namespace VNext.Entity
{
    public static class ExpressionToSqlExtension
    {
        #region Like
        /// <summary>
        /// LIKE
        /// </summary>
        /// <param name="this">扩展对象自身</param>
        /// <param name="value">包含的字符串</param>
        /// <returns>bool</returns>
        public static bool Like(this object @this, string value) => true;

        /// <summary>
        /// LIKE '% _ _ _'
        /// </summary>
        /// <param name="this">扩展对象自身</param>
        /// <param name="value">包含的字符串</param>
        /// <returns>bool</returns>
        public static bool LikeLeft(this object @this, string value) => true;

        /// <summary>
        /// LIKE '_ _ _ %'
        /// </summary>
        /// <param name="this">扩展对象自身</param>
        /// <param name="value">包含的字符串</param>
        /// <returns>bool</returns>
        public static bool LikeRight(this object @this, string value) => true;

        /// <summary>
        /// NOT LIKE
        /// </summary>
        /// <param name="this">扩展对象自身</param>
        /// <param name="value">包含的字符串</param>
        /// <returns>bool</returns>
        public static bool NotLike(this object @this, string value) => true;
        #endregion

        #region In
        /// <summary>
        /// IN
        /// </summary>
        /// <typeparam name="T">IN数组里面的数据类型</typeparam>
        /// <param name="this">扩展对象自身</param>
        /// <param name="array">IN数组</param>
        /// <returns>bool</returns>
        public static bool In<T>(this object @this, params T[] array) => true;

        /// <summary>
        /// NOT IN
        /// </summary>
        /// <typeparam name="T">NOT IN数组里面的数据类型</typeparam>
        /// <param name="this">扩展对象自身</param>
        /// <param name="array">NOT IN数组</param>
        /// <returns>bool</returns>
        public static bool NotIn<T>(this object @this, params T[] array) => true;
        #endregion

        #region ToObject
        /// <summary>
        /// 转换Expression为object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object ToObject(this Expression @this)
        {
            object obj = null;
            switch (@this.NodeType)
            {
                case ExpressionType.Constant:
                    obj = (@this as ConstantExpression)?.Value;
                    break;
                case ExpressionType.Convert:
                    obj = (@this as UnaryExpression)?.Operand?.ToObject();
                    break;
                default:
                    var isNullable = @this.Type.IsNullableType();
                    switch (@this.Type.GetUnNullableType().Name.ToLower())
                    {
                        case "string":
                            obj = @this.ToLambda<Func<string>>().Compile().Invoke();
                            break;
                        case "int16":
                            if (isNullable)
                                obj = @this.ToLambda<Func<short?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<short>>().Compile().Invoke();
                            break;
                        case "int32":
                            if (isNullable)
                                obj = @this.ToLambda<Func<int?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<int>>().Compile().Invoke();
                            break;
                        case "int64":
                            if (isNullable)
                                obj = @this.ToLambda<Func<long?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<long>>().Compile().Invoke();
                            break;
                        case "decimal":
                            if (isNullable)
                                obj = @this.ToLambda<Func<decimal?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<decimal>>().Compile().Invoke();
                            break;
                        case "double":
                            if (isNullable)
                                obj = @this.ToLambda<Func<double?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<double>>().Compile().Invoke();
                            break;
                        case "datetime":
                            if (isNullable)
                                obj = @this.ToLambda<Func<DateTime?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<DateTime>>().Compile().Invoke();
                            break;
                        case "boolean":
                            if (isNullable)
                                obj = @this.ToLambda<Func<bool?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<bool>>().Compile().Invoke();
                            break;
                        case "byte":
                            if (isNullable)
                                obj = @this.ToLambda<Func<byte?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<byte>>().Compile().Invoke();
                            break;
                        case "char":
                            if (isNullable)
                                obj = @this.ToLambda<Func<char?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<char>>().Compile().Invoke();
                            break;
                        case "single":
                            if (isNullable)
                                obj = @this.ToLambda<Func<float?>>().Compile().Invoke();
                            else
                                obj = @this.ToLambda<Func<float>>().Compile().Invoke();
                            break;
                        default:
                            obj = @this.ToLambda<Func<object>>().Compile().Invoke();
                            break;
                    }
                    break;
            }
            return obj;
        }
        #endregion

        #region ToLambda
        /// <summary>
        /// ToLambda
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Expression<T> ToLambda<T>(this Expression @this, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<T>(@this, parameters);
        }
        #endregion

        #region ToDbParameters
        /// <summary>
        /// An IDictionary&lt;string,object&gt; extension method that converts this object to a database parameters.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="command">The command.</param>        
        /// <returns>The given data converted to a DbParameter[].</returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbCommand command)
        {
            if (@this?.Count > 0)
            {
                return @this.Select(x =>
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = x.Key;
                    parameter.Value = x.Value;
                    return parameter;
                }).ToArray();
            }
            return null;
        }

        /// <summary>
        ///  An IDictionary&lt;string,object&gt; extension method that converts this object to a database parameters.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="connection">The connection.</param>        
        /// <returns>The given data converted to a DbParameter[].</returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbConnection connection)
        {
            if (@this?.Count > 0)
            {
                var command = connection.CreateCommand();
                return @this.Select(x =>
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = x.Key;
                    parameter.Value = x.Value;
                    return parameter;
                }).ToArray();
            }
            return null;
        }
        #endregion       
    }
}