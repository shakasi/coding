using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Options
{
    public static class ConfigureExtensions
    {
        public static IConfigurationRoot ToConfiguration(this ConfigureOption configureOption)
        {
            Check.NotNull(configureOption, nameof(configureOption));
            if (configureOption.Type == ConfigureType.Json)
            {
                return configureOption.GetJsonConfig();
            }
            else if (configureOption.Type == ConfigureType.Xml)
            {
                return configureOption.GetXmlConfig();
            }
            else
            {
                throw new Exception("不支持的文件配置类型");
            }
        }

        /// <summary>
        /// 获取Json配置文件
        /// </summary>
        /// <param name="configOption">配置构建起选项</param>
        private static IConfigurationRoot GetJsonConfig(this ConfigureOption configOption)
        {
            Check.NotNull(configOption?.FileName, nameof(configOption.FileName));
            var path = string.IsNullOrWhiteSpace(configOption.BasePath)
               ? Directory.GetCurrentDirectory()
               : Path.Combine(Directory.GetCurrentDirectory(), configOption.BasePath);

            var configurationBuilder = new ConfigurationBuilder().SetBasePath(path)
            .AddJsonFile($"{configOption.FileName}.{configOption.Type.ToString().ToLower()}", true, true);

            if (!string.IsNullOrEmpty(configOption.Env))
            {
                configurationBuilder.AddJsonFile($"{configOption.FileName}.{configOption.Env.ToLower()}.{configOption.Type.ToString().ToLower()}", true, true);
            }
            return configurationBuilder.Build();
        }

        /// <summary>
        /// 获取Xml配置文件
        /// </summary>
        /// <param name="configOption">配置构建起选项</param>
        /// <returns></returns>
        private static IConfigurationRoot GetXmlConfig(this ConfigureOption configOption)
        {
            Check.NotNull(configOption, nameof(configOption));
            var path = string.IsNullOrWhiteSpace(configOption.BasePath)
              ? Directory.GetCurrentDirectory()
              : Path.Combine(Directory.GetCurrentDirectory(), configOption.BasePath);

            var configurationBuilder = new ConfigurationBuilder().SetFileProvider(new PhysicalFileProvider(path))
                .AddXmlFile($"{configOption.FileName}.{configOption.Type.ToString().ToLower()}", true, true);
            if (!string.IsNullOrWhiteSpace(configOption.Env))
            {
                configurationBuilder.AddXmlFile($"{configOption.FileName}.{configOption.Env.ToLower()}.{configOption.Type.ToString().ToLower()}", true, true);
            }

            return configurationBuilder.Build();
        }
    }
}
