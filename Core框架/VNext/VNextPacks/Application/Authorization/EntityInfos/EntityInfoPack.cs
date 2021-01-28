using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.ComponentModel;
using VNext.Packs;
using VNext.Systems;

namespace VNext.Authorization.EntityInfos
{
    /// <summary>
    /// 实体信息模块
    /// </summary>
    [Description("数据实体模块")]
    //[DependsOnPacks(typeof(SystemPack))]
    public class EntityInfoPack : Pack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<IEntityInfoHandler, EntityInfoHandler>();

            return base.AddServices(services);
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            IEntityInfoHandler handler = provider.GetService<IEntityInfoHandler>();
            handler.Initialize();
            IsEnabled = true;
        }
    }
}