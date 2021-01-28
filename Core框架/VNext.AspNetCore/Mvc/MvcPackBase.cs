using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using VNext.AspNetCore.Mvc.Filters;
using VNext.Dependency;
using VNext.Packs;
using VNext.Threading;

namespace VNext.AspNetCore.Mvc
{
    /// <summary>
    /// Mvc模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class MvcPackBase : AspPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services = AddCors(services);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddRouting(opts => opts.LowercaseUrls = true);

            services.AddScoped<UnitOfWorkFilterImpl>();
            
            //https配置
            //services.AddHttpsRedirection(opts => opts.HttpsPort = 443);

            services.AddScoped<UnitOfWorkAttribute>();
            services.TryAddSingleton<IVerifyCodeService, VerifyCodeService>();
            services.TryAddSingleton<IScopeServiceResolver, HttpContextScopeServiceResolver>();
            services.Replace<ICancellationTokenProvider, HttpContextCancellationTokenProvider>(ServiceLifetime.Singleton);
            services.Replace<IHybridServiceScopeFactory, HttpContextServiceScopeFactory>(ServiceLifetime.Singleton);

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseRouting();
            UseCors(app);

            IsEnabled = true;
        }

        /// <summary>
        /// 重写以实现添加Cors服务
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected virtual IServiceCollection AddCors(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 重写以应用Cors
        /// </summary>
        protected virtual IApplicationBuilder UseCors(IApplicationBuilder app)
        {
            return app;
        }
    }
}