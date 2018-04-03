using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cuscapi.Utils
{
    public class FactoryHelper
    {
        public static object GetInstance(string strDll, string name)
        {
            //判断目录是否存在
            if (string.IsNullOrEmpty(strDll))
            {
                throw new InvalidOperationException("没有从配置文件中获得工厂程序集名称");
            }
            string className = string.Format("{0}.{1}", strDll, name);
            Assembly assembly = System.Reflection.Assembly.Load(strDll);
            return assembly.CreateInstance(className);
        }
    }
}
