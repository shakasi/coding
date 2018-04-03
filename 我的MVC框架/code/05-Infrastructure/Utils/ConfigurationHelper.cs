using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace Shaka.Utils
{
    public class ConfigurationHelper
    {
        //获得配置文件
        private System.Configuration.Configuration _config;

        public ConfigurationHelper()
            : this(HttpContext.Current.Request.ApplicationPath)
        { }

        public ConfigurationHelper(string path)
        {
            _config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
        }

        /// <summary>
        /// 设置应用程序配置节点,如果已经存在此节点,则会修改该节点的值,否则添加此节点
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <param name="value">节点值</param>
        public void SetAppSetting(string key, string value)
        {
            AppSettingsSection appSection = (AppSettingsSection)_config.GetSection("appSettings");
            if (appSection.Settings[key] == null)//如果不存在此节点则添加
            {
                appSection.Settings.Add(key, value);
            }
            else//如果存在此节点，则修改
            {
                appSection.Settings[key].Value = value;
            }
        }

        /// <summary>
        /// 设置数据库连接字符串节点,如果不存在此节点,则会添加此节点及对应的值,存在则修改
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="connectionString">节点值</param>
        public void SetConnectionStrings(string name, string connectionString)
        {
            ConnectionStringsSection connSection = (ConnectionStringsSection)_config.GetSection("connectionStrings");
            if (connSection.ConnectionStrings[name] == null)//如果不存在此节点则添加
            {
                ConnectionStringSettings connSettings = new ConnectionStringSettings(name, connectionString);
                connSection.ConnectionStrings.Add(connSettings);
            }
            else//如果存在此节点，则修改
            {
                connSection.ConnectionStrings[name].ConnectionString = connectionString;
            }
        }

        public void Save()
        {
            _config.Save();
        }
    }
}
