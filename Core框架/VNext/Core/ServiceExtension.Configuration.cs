using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using VNext.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 从IServiceCollection中,获取<see cref="IConfiguration"/>配置信息
        /// </summary>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetSingletonOrDefault<IConfiguration>();
        }

        #region IConfiguration扩展方法

        /// <summary>
        /// 读取指定节点信息
        /// </summary>
        /// <param name="configuration">配置信息【键值对集合】</param>
        /// <param name="key">节点名称，多节点以:分隔</param>
        public static string GetString(this IConfiguration configuration, string key)
        {
            return configuration[key];
        }

        /// <summary>
        /// 读取指定节点的简单数据类型的值
        /// </summary>
        /// <param name="configuration">配置信息【键值对集合】</param>
        /// <param name="key">节点名称，多节点以:分隔</param>
        /// <param name="defaultValue">默认值，读取失败时使用</param>
        public static T GetValue<T>(this IConfiguration configuration, string key, T defaultValue = default)
        {
            string str = configuration[key];
            return str.CastTo<T>(defaultValue);
        }

        /// <summary>
        /// 读取指定节点的复杂类型的值，并绑定到指定的空实例上
        /// </summary>
        /// <typeparam name="T">复杂类型</typeparam>
        /// <param name="configuration">配置信息【键值对集合】</param>
        /// <param name="key">节点名称，多节点以:分隔</param>
        /// <param name="instance">要绑定的空实例</param>
        /// <returns></returns>
        public static T GetInstance<T>(this IConfiguration configuration, string key, T instance = default) where T : new()
        {
            var config = configuration.GetSection(key);
            if (config.Exists())
            {
                instance ??= new T();
                config.Bind(instance);
            }
            return instance;
        }

        #endregion
    }
}