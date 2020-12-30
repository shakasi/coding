using System;

namespace VNext.Identity.JwtBearer
{
    /// <summary>
    /// 刷新Token信息
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// 获取或设置 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime EndUtcTime { get; set; }
    }
}