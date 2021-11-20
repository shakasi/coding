using Microsoft.Extensions.DependencyInjection;
using System;
using VNext.Entity;

namespace DoCare.WebApi.Startups
{
    public class DesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        public DesignTimeDefaultDbContextFactory()
       : base(null)
        { }

        public DesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        /// <summary>
        /// 创建设计时使用的ServiceProvider，主要用于执行 Add-Migration 功能
        /// </summary>
        /// <returns></returns>
        protected override IServiceProvider CreateDesignTimeServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);

            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}