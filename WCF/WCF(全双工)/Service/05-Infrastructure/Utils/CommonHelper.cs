using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;

namespace Cuscapi.Utils
{
    public class CommonHelper
    {
        /// <summary>
        /// 获得程序目录的虚拟目录（比如“01-UserInterface”）的父路径
        /// </summary>
        /// <returns></returns>
        public static string GetBaseDirctory()
        {
            string strBasePath = System.AppDomain.CurrentDomain.BaseDirectory;
            strBasePath = strBasePath.Substring(0, Regex.Match(strBasePath, @"\d{2}-").Index);
            return strBasePath;
        }

        /// <summary>
        /// 把集合里的某列拼接成逗号隔开的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetLstToStr<T>(T t, string colName) where T : IEnumerable
        {
            StringBuilder sbCol = new StringBuilder();
            foreach (object obj in t)
            {
                sbCol.Append("," + obj.GetType().GetProperty(colName).GetValue(obj, null));
            }

            return sbCol.ToString().TrimStart(',');
        }

        /// <summary>
        /// 查找指定文件夹下所有指定后缀名的文件
        /// </summary>
        /// <param name="lstFileInfo"></param>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="extension">后缀名，多个用‘|’隔开</param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(ref List<FileInfo> lstFileInfo, string folderPath, string extension)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            string[] arrExtension = extension.Split('|');
            //子文件
            foreach (string subExtension in arrExtension)
            {
                FileInfo[] arrFileinfo = dirInfo.GetFiles(subExtension, SearchOption.AllDirectories);
                lstFileInfo.AddRange(arrFileinfo.ToList<FileInfo>());
            }
            return lstFileInfo;
        }

        public static string GetDBDateTimeStr(DateTime dt)
        {
            return dt.ToString("yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }

        public DateTime GetDateTimeToDB()
        {
            return DateTime.ParseExact(DateTime.Now.ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}