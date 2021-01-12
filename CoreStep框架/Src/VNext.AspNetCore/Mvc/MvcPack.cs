using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;


namespace VNext.AspNetCore.Mvc
{
    /// <summary>
    /// MVC模块
    /// </summary>
    [Description("MVC模块")]
    public class MvcPack : MvcPackBase
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;

        #region 跨域
        /// <summary>
        /// 重写以实现添加Cors服务
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddCors(IServiceCollection services)
        {
            services.AddCors(option =>
                 option.AddPolicy("*",
                     builder => builder
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowAnyOrigin()//.AllowCredentials()
                         ));
            return services;
        }

        /// <summary>
        /// 重写以应用Cors
        /// </summary>
        protected override IApplicationBuilder UseCors(IApplicationBuilder app)
        {
            app.UseCors("*");
            return app;
        }
        #endregion
    }
}