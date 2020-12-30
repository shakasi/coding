using System.ComponentModel;

namespace VNext.Options
{
    /// <summary>
    /// 配置构建器选项
    /// </summary>
    public class ConfigureOption
    {
        /// <summary>
        /// 文件名。默认值：appsettings
        /// </summary>
        public string FileName { get; set; } = "appsettings";

        /// <summary>
        /// 环境名称。支持：development(开发环境)、staging(演示环境)、production(生产环境)
        /// </summary>
        public string Env { get; set; } = "development";

        /// <summary>
        /// 基本路径。用于读取配置文件的根路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public ConfigureType Type { get; set; }
    }

    /// <summary>
    /// 文件配置类型
    /// </summary>
    public enum ConfigureType
    {
        [Description("Json配置类型")]
        Json = 0,
        [Description("Xml配置类型")]
        Xml
    }
}
