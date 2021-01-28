using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using VNext.Extensions;

namespace VNext.Extensions
{
    /// <summary>
    /// 枚举类型元数据
    /// </summary>
    public class EnumMetadata
    {
        /// <summary>
        /// 初始化一个<see cref="EnumMetadata"/>类型的新实例
        /// </summary>
        public EnumMetadata()
        { }

        /// <summary>
        /// 初始化一个<see cref="EnumMetadata"/>类型的新实例
        /// </summary>
        public EnumMetadata(Enum enumItem)
        {
            if (enumItem == null)
            {
                return;
            }
            Value = enumItem.CastTo<int>();
            Name = enumItem.ToString();
            Display = enumItem.ToDescription();
        }

        /// <summary>
        /// 获取或设置 枚举值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 获取或设置 枚举名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        public string Display { get; set; }
    }

    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, EnumMetadata>> EnumAbout
         = new ConcurrentDictionary<Type, Dictionary<string, EnumMetadata>>();

        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.GetDescription() : value.ToString();
        }

        /// <summary>
        /// 获取枚举项上自定义的Attribute
        /// </summary>
        public static T GetCustomAttribute<T>(this Enum e, bool inherit) where T : Attribute
        {
            return e.GetType().GetField(e.ToString()).GetCustomAttribute<T>(inherit);
        }

        public static Dictionary<string, EnumMetadata> GetItemList(this Enum value)
        {
            Type eType = value.GetType();
            if (!EnumAbout.ContainsKey(eType))
            {
                Type valueType = Enum.GetUnderlyingType(eType);
                var enums = Enum.GetValues(eType);
                Dictionary<string, EnumMetadata> tmpList = new Dictionary<string, EnumMetadata>();
                foreach (Enum e in enums)
                    tmpList.Add(Convert.ChangeType(e, valueType).ToString(), new EnumMetadata(e));
                EnumAbout.TryAdd(eType, tmpList);
            }
            return EnumAbout[eType];
        }

        /// <summary>
        /// 根据备注信息获取枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static Enum GetEnumByDesc(this Type enumType, string enumValue)
        {
            Enum result = null;
            foreach (object enumObject in Enum.GetValues(enumType))
            {
                Enum e = (Enum)enumObject;
                if (ToDescription(e) == enumValue)
                {
                    result = e;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据枚举值得到枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strEnum"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int strEnum)
        {
            return (T)Enum.Parse(typeof(T), strEnum.ToString());
        }

        public static T ToEnum<T>(this string strEnum)
        {
            return (T)Enum.Parse(typeof(T), strEnum);
        }

        /// <summary> 获取枚举类子项描述信息 </summary>
        /// <param name="enumtype">枚举类型</param>
        /// <param name="strVlaue">值</param>        
        public static string GetEnumDescriptionByValue(Type enumtype, string strVlaue)
        {
            if (string.IsNullOrWhiteSpace(strVlaue))
            {
                return string.Empty;
            }
            FieldInfo fieldinfo;
            DescriptionAttribute decAttr;
            foreach (Enum enumValue in Enum.GetValues(enumtype))
            {
                fieldinfo = enumtype.GetField((enumValue.ToString()));

                decAttr = enumtype.GetType().GetField(enumtype.ToString()).GetCustomAttribute<DescriptionAttribute>();
                if (decAttr != null && strVlaue.Equals(enumValue.ToString("d")))
                {
                    return decAttr.Description;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的值字符串信息，根据传入的枚举值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int GetEnumInt(Enum e)
        {
            return Convert.ToInt32(e);

        }

        /// <summary>
        /// 获取枚举类子项名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumValue">值</param>
        /// <returns></returns>
        public static Enum GetEnumByValue(Type enumType, int enumValue)
        {
            Enum result = null;
            foreach (object enumObject in Enum.GetValues(enumType))
            {
                Enum e = (Enum)enumObject;
                if (GetEnumInt(e) == enumValue)
                {
                    result = e;
                    break;
                }
            }
            return result;
        }

        public static int GetEnumValueByName(Type enumType, string name)
        {
            int enumValue = 0;
            foreach (object enumObject in Enum.GetValues(enumType))
            {
                Enum e = (Enum)enumObject;
                if (e.ToString() == name)
                {
                    enumValue = GetEnumInt(e);
                    break;
                }
            }
            return enumValue;
        }

        /// <summary>
        /// 获取枚举所有hash值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<int> GetEnumHashList(Type enumType)
        {
            var enumList = new List<int>();
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                enumList.Add(Convert.ToInt32(enumValue));
            }
            return enumList;
        }
    }
}