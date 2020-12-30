namespace VNext.Options
{
    /// <summary>
    /// Redis选项
    /// </summary>
    public class RedisOption
    {
        /// <summary>
        /// 获取或设置 Redis连接配置
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// 获取或设置 Redis实例名称
        /// </summary>
        public string InstanceName { get; set; }
    }
}