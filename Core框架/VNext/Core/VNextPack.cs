using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using VNext.Finders;
using VNext.Options;
using VNext.Systems;

namespace VNext.Packs
{
    /// <summary>
    /// VNext核心模块
    /// </summary>
    [Description("核心模块")]
    public class VNextPack : Pack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Core;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //Finders
            services.TryAddSingleton<IAppAssemblyFinder, AppAssemblyFinder>();
            services.TryAddSingleton<IMethodInfoFinder, PublicInstanceMethodInfoFinder>();

            //Options
            services.TryAddSingleton<IConfigureOptions<VNextOptions>, VNextOptionsSetup>();

            //Packs
            services.TryAddSingleton<IPackTypeFinder, PackTypeFinder>();
            services.TryAddSingleton<IPackBuilder, PackBuilder>();

            //StartupLogger
            services.TryAddSingleton<StartupLogger>();

            //Systems
            services.TryAddScoped<IKeyValueStore, KeyValueStore>();

            
            return services;
        }
    }
}