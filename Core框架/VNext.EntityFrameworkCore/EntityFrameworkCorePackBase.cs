using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using VNext.EventBuses;
using VNext.Finders;
using VNext.Packs;

namespace VNext.Entity
{
    /// <summary>
    /// EntityFrameworkCore基模块
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack))]
    public abstract class EntityFrameworkCorePackBase : Pack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IAuditEntityProvider, AuditEntityProvider>();
            services.TryAddSingleton<IOutEntityTypeFinder, OutEntityTypeFinder>();
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.TryAddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.TryAddSingleton<IEntityConfigurationTypeFinder, EntityConfigurationTypeFinder>();
            services.TryAddSingleton<IEntityManager, EntityManager>();
            services.AddSingleton<DbContextModelCache>();
            services.AddVNextDbContext<DefaultDbContext>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            IEntityManager manager = provider.GetService<IEntityManager>();
            manager.Initialize();

            var outEntityFinder = provider.GetService<IOutEntityTypeFinder>();
            if (outEntityFinder != null)
            {
                var outEntities = outEntityFinder.FindAll();
                outEntities.ToList().ForEach(type =>
                {
                    ColumnMapper.SetMapper(type);
                });
            }
            IsEnabled = true;
        }
    }
}