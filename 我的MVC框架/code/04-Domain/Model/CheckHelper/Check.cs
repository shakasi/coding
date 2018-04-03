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
                        return string.Format("{0}:{1}", proShowName, attribute.ProShowMsg);
                    }
                }
            }
            return errorMsg;
        }
    }

    public class CheckAttribute : Attribute
    {
        private bool _checkEmpty = false;
        public bool CheckEmpty
        {
            get { return this._checkEmpty; }
            set { this._checkEmpty = value; }
        }

        private int _minLength = 0;
        public int MinLength
        {
            get { return this._minLength; }
            set { this._minLength = value; }
        }

        private int _maxLength = 0;
        public int MaxLength
        {
            get { return this._maxLength; }
            set { this._maxLength = value; }
        }

        private string _regexStr = string.Empty;
        public string RegexStr
        {
            get { return this._regexStr; }
            set { this._regexStr = value; }
        }

        public string ProShowName { get; set; }

        public string _proShowMsg = "格式不正确";
        /// <summary>
        /// 正则表达式这种信息不确定会用到
        /// </summary>
        public string ProShowMsg
        {
            get { return this._proShowMsg; }
            set { this._proShowMsg = value; }
        }
    }
}
