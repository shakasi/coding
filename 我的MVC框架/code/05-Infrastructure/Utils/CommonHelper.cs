using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;

namespace Shaka.Utils
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

        /// <summary>
        /// 取行号 注意：和ex.StackTrace.Substring(ex.StackTrace.IndexOf("行号") + 3)一样依赖.pdb
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 把字符串数组中的数据,转换成另外一种泛型类型.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static dynamic TryParserArray(List<string> values, Type t)
        {
            //string[] searchArray = value.Split(',');
            var genericType = typeof(List<>).MakeGenericType(t);
            var list = Activator.CreateInstance(genericType);
            var addMethod = genericType.GetMethod("Add");

            foreach (var l in values)
            {
                try
                {
                    var dValue = Convert.ChangeType(l, t);
                    //dList.Add(dValue);
                    addMethod.Invoke(list, new object[] { dValue });
                }
                catch { }
            }
            return list;

            #region 以下使用此方法示例
            //使用在牛B闪闪的linq表达式中.实现 In 表达式
            //var Values = new List<string>() { "18", "19", "20" };
            //var rType = typeof(int);

            //var genericType = typeof(List<>).MakeGenericType(rType);//等价于 typeof(List<int>) 只是,int可以根据Type动态改变了.更加灵活一些了.

            ////用dynamic做装箱拆箱 
            //var searchList = TryParserArray(Values, rType);//这里返回的是一个泛型链表.泛型内类型为 rType指定的类型.  类型已经变成了List<int>类型

            //if (searchList.Count > 0)
            //{
            //    var elementList = Expression.Constant(searchList, genericType); //这个等价于 (p=> searchList.Contains(p.age)) 中的searchList
            //                                                                    //Expression convertExpression = Expression.Convert(p, rType);
            //    list.Add(Expression.Call(elementList, "Contains", null, p));//这个等价于 (p=> searchList.Contains(p.age)) 中的 searchList.Contains(p.age)
            //}
            #endregion
        }
    }
}
