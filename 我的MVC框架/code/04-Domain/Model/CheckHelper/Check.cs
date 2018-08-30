using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shaka.Model
{
    /// <summary>
    /// 需要验证的地方写 Console.WriteLine(CheckHelper.GetErrorMessage(实体类));
    /// </summary>
    public class CheckHelper
    {
        public static string GetErrorMessage<T>(T t) where T : new()
        {
            string errorMsg = string.Empty;
            PropertyInfo[] proArray = t.GetType().GetProperties();

            foreach (PropertyInfo pro in proArray)
            {
                object[] checkAttributeArray = pro.GetCustomAttributes(false);
                if (checkAttributeArray == null || checkAttributeArray.Length == 0)
                {
                    continue;
                }
                CheckAttribute attribute = checkAttributeArray.First() as CheckAttribute;
                string proShowName = attribute.ProShowName;
                if (proShowName == null)
                {
                    proShowName = pro.Name;
                }

                if (attribute.CheckEmpty)
                {
                    string obj = pro.GetValue(t, null) as string;
                    if (string.IsNullOrEmpty(obj))
                    {
                        return string.Format("{0}:不能为空", proShowName);
                    }
                }

                if (attribute.MinLength != 0)
                {
                    string obj = pro.GetValue(t, null) as string;
                    if (obj != null && obj.Length < attribute.MinLength)
                    {
                        return string.Format("{0}:不能小于最小长度{1}", proShowName, attribute.MinLength);
                    }
                }

                if (attribute.MaxLength != 0)
                {
                    string obj = pro.GetValue(t, null) as string;
                    if (obj != null && obj.Length > attribute.MaxLength)
                    {
                        return string.Format("{0}:不能超过最大长度{1}", proShowName, attribute.MaxLength);
                    }
                }

                // 对于判断数字邮箱等都可以通过正则表达式
                if (attribute.RegexStr != null)
                {
                    string obj = pro.GetValue(t, null) as string;
                    Regex regex = new Regex(attribute.RegexStr);
                    if (obj != null && !regex.IsMatch(obj))
                    {
                        return string.Format("{0}:{1}", proShowName, "格式不正确");
                    }
                }
            }
            return errorMsg;
        }
    }

    public class CheckAttribute : Attribute
    {
        public bool CheckEmpty { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public string RegexStr { get; set; }

        public string ProShowName { get; set; }
    }
}
