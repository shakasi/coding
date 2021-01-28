using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace VNext.Options
{
    /// <summary>
    /// 单列:Lazy模式
    /// </summary>
    public static class ConfigurationReader
    {
        #region appsettings.json + appsettings.hospital.json

        private static Lazy<IConfiguration> appsettings = new Lazy<IConfiguration>(() => new ConfigureOption()
        {
            Type = ConfigureType.Json,
            FileName = "appsettings",
            Env = "hospital",
            BasePath = string.Empty
        }.ToConfiguration());

        public static IConfiguration Appsettings
        {
            get
            {
                return appsettings.Value;
            }
        }

        private static Lazy<VNextOptions> vNextOptions = new Lazy<VNextOptions>(() => Appsettings.GetVNextOptions());

        public static VNextOptions VNextOptions
        {
            get
            {
                return vNextOptions.Value;
            }
        }

        #endregion

    }
}