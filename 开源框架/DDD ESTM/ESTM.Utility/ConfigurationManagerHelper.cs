using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.Utility
{
    public class ConfigurationManagerHelper
    {

        #region 私有属性

        private static ConfigurationManagerHelper _instance;
        private static object _lock = new object();
        private string _IP = string.Empty;
        private string _Port = string.Empty;

        #endregion

        #region 公有属性

        private Dictionary<string, string> _appSettings = new Dictionary<string, string>();

        /// <summary>
        /// AppSetting值
        /// </summary>
        public Dictionary<string, string> AppSettings
        {
            get { return _appSettings; }
            set { _appSettings = value; }
        }

        #endregion

        #region 事件/委托等

        #endregion

        #region 构造函数
        private ConfigurationManagerHelper()
        {
            //获取相关配置信息
            string[] strs = System.Configuration.ConfigurationManager.AppSettings.AllKeys;

            foreach (string key in strs)
            {
                if (!AppSettings.ContainsKey(key))
                {
                    AppSettings.Add(key, System.Configuration.ConfigurationManager.AppSettings[key]);
                }
            }
            if (AppSettings.ContainsKey("IP"))
            {
                _IP = AppSettings["IP"];
            }
            if (AppSettings.ContainsKey("Port"))
            {
                _Port = AppSettings["Port"];
            }
        }

        #endregion

        #region 私有方法

        #endregion

        #region 公有方法
        /// <summary>
        /// 单调函数
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManagerHelper GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManagerHelper();
                    }
                }
            }
            return _instance;
        }

        public string GetAddressByKey(Type type)
        {
            if (AppSettings.ContainsKey(type.Name))
            {
                return AppSettings[type.Name];
            }
            else
            {
                string wcfUrl = "http://" + _IP + ":" + _Port + "/" + type.FullName.Replace('.', '/') + "/";
                AppSettings.Add(type.Name, wcfUrl);
                return wcfUrl;
            }
        }

        public string GetValueByKey(string key)
        {
            if (AppSettings.ContainsKey(key))
            {
                return AppSettings[key];
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
