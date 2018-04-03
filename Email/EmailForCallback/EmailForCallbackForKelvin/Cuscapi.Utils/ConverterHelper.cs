using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cuscapi.Utils
{
    public static class ConverterHelper
    {
        private static Dictionary<string, object> _parseDic = new Dictionary<string, object>();
        private delegate bool TryParseEventHandle<T>(string s, out T tResult) where T : IConvertible;

        /// <summary>
        /// 转换为其他继承IConvertible的类型
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <param name="success">是否成功</param>
        /// <returns></returns>
        public static T To<T>(this IConvertible value, out bool success) where T : IConvertible
        {
            if (value == null)
            {
                success = true;
                return default(T);
            }

            Type tResult = typeof(T);
            if (tResult == typeof(string))
            {
                success = true;
                return (T)(object)value.ToString();
            }

            T result;
            TryParseEventHandle<T> tryParseDelegate;
            if (_parseDic.ContainsKey(tResult.FullName))
            {
                tryParseDelegate = (TryParseEventHandle<T>)_parseDic[tResult.FullName];
            }
            else
            {
                MethodInfo mTryParse = null;
                if (tResult.BaseType == typeof(Enum))
                {
                    //enum 拿不到GetMethod("TryParse"）,所以多此分支，未加入字典，或许有好办法
                    try
                    {
                        result = (T)Enum.Parse(tResult, value as string);
                        success = true;
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("{0} 不可以转换为枚举:{1}，错误：{2}", value, tResult,ex.Message));
                    }
                }
                else
                {
                    mTryParse = tResult.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder,
                        new Type[] { typeof(string), tResult.MakeByRefType() }, new ParameterModifier[] { new ParameterModifier(2) });
                }

                tryParseDelegate = (TryParseEventHandle<T>)Delegate.CreateDelegate(typeof(TryParseEventHandle<T>), mTryParse);
                _parseDic.Add(tResult.FullName, tryParseDelegate);
            }

            success = tryParseDelegate(value.ToString(), out result);

            return result;
        }

        /// <summary>
        /// 转换为其他继承IConvertible的类型
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static T To<T>(this IConvertible value) where T : IConvertible
        {
            bool success;
            return To<T>(value, out success);
        }
    }
}