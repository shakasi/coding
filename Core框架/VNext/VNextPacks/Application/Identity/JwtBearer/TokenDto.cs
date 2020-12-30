using System.ComponentModel.DataAnnotations;

namespace VNext.Identity.JwtBearer
{
    /// <summary>
    /// Token请求DTO
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// 获取或设置 授权类型
        /// </summary>
        [Required]
        public string GrantType { get; set; }

        /// <summary>
        /// 获取或设置 账户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 获取或设置 客户端类型
        /// </summary>
        public RequestClientType ClientType { get; set; }

        /// <summary>
        /// 获取或设置 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}