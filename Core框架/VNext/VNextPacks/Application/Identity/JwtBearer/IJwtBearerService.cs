﻿using System.Threading.Tasks;

namespace VNext.Identity.JwtBearer
{
    /// <summary>
    /// 定义JwtBearer服务，负责JwtToken的创建，验证，刷新等业务
    /// </summary>
    public interface IJwtBearerService
    {
        /// <summary>
        /// 创建指定用户的JwtToken信息
        /// </summary>
        /// <param name="userId">用户编号的字符串</param>
        /// <param name="userName">用户名的字符串</param>
        /// <param name="clientType">客户端类型</param>
        /// <returns>JwtToken信息</returns>
        Task<JsonWebToken> CreateToken(string userId, string userName, RequestClientType clientType = RequestClientType.Browser);

        /// <summary>
        /// 使用RefreshToken获取新的JwtToken信息
        /// </summary>
        /// <param name="refreshToken">刷新Token字符串</param>
        /// <returns>JwtToken信息</returns>
        Task<JsonWebToken> RefreshToken(string refreshToken);
    }
}