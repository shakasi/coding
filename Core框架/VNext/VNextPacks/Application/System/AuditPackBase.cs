using Microsoft.Extensions.DependencyInjection;
using VNext.EventBuses;
using VNext.Packs;

namespace VNext.Systems
{
    /// <summary>
    /// 审计模块基类
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack))]
    public abstract class AuditPackBase : Pack
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
            services.AddEventHandler<AuditEntityEventHandler>();
            return base.AddServices(services);
        }
    }
}