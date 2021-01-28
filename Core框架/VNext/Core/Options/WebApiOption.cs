namespace VNext.Options
{
    /// <summary>
    /// 调用第三方webapi配置
    /// </summary>
    public class WebApiOption
    {
        /// <summary>
        /// 唯一约束标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// host地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 跟路由
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 超时时间设置
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 内容大小限制
        /// </summary>
        public int ContentSize { get; set; }

        /// <summary>
        /// 分配到的客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 分配到的客户端密码
        /// </summary>
        public string ClientSecurity { get; set; }
    }
}
