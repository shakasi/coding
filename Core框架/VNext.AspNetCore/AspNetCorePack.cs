﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.ComponentModel;
using System.Security.Principal;
using VNext.Packs;

namespace VNext.AspNetCore
{
    /// <summary>
    /// AspNetCore模块
    /// </summary>
    [Description("AspNetCore模块")]
    public class AspNetCorePack : Pack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Core;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动
        /// </summary>
        public override int Order => 3;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //注入当前用户，替换Thread.CurrentPrincipal的作用
            services.AddTransient<IPrincipal>(provider =>
            {
                IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });

            return services;
        }
    }
}