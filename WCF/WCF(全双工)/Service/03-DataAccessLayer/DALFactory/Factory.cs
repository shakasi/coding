using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;
using Cuscapi.Utils;
using Cuscapi.IDAL;

namespace Cuscapi.DALFactory
{
    /// <summary>
    /// 工厂类：创建访问数据库的实例对象（反射程序集）
    /// </summary>
    public class Factory
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //程序集名字(要求和空间名字一致)
        private static readonly string _strDll;
        private static readonly Assembly _assembly;

        static Factory()
        {
            _strDll = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
            //判断目录是否存在
            if (string.IsNullOrEmpty(_strDll))
            {
                _log.Fatal("没有从配置文件中获得工厂程序集名称");
                throw new InvalidOperationException();
            }
            _assembly = System.Reflection.Assembly.Load(_strDll);
        }

        public static Cuscapi.IDAL.IUser GetUserDAL()
        {
            string className = string.Format("{0}.{1}", _strDll, "UserDAL");
            return _assembly.CreateInstance(className) as IUser;
        }

        public static Cuscapi.IDAL.IGroup GetGroupDAL()
        {
            try
            {
                string className = string.Format("{0}.{1}", _strDll, "GroupDAL");
                return _assembly.CreateInstance(className) as IGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Cuscapi.IDAL.IStore GetStoreDAL()
        {
            try
            {
                string className = string.Format("{0}.{1}", _strDll, "StoreDAL");
                return _assembly.CreateInstance(className) as IStore;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Cuscapi.IDAL.IWCFDataSync GetWCFDataSyncDAL()
        {
            string className = string.Format("{0}.{1}", _strDll, "WCFDataSyncDAL");
            return _assembly.CreateInstance(className) as IWCFDataSync;
        }
    }
}