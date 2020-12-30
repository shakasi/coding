using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace VNext.Entity
{
    /// <summary>
    /// 设计时数据上下文实例工厂基类，用于执行数据迁移
    /// </summary>
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="DesignTimeDbContextFactoryBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DesignTimeDbContextFactoryBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            _logger = serviceProvider?.GetLogger(GetType());
        }

        protected IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 创建一个数据上下文实例
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual TDbContext CreateDbContext(string[] args)
        {
            string migrationAssemblyName = GetType().Assembly.GetName().Name;
            ServiceExtensions.MigrationAssemblyName = migrationAssemblyName;
            Console.WriteLine($@"MigrationAssembly: {migrationAssemblyName}");
            
            ServiceProvider ??= CreateDesignTimeServiceProvider();

            IEntityManager entityManager = ServiceProvider.GetService<IEntityManager>();
            entityManager.Initialize();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<TDbContext>();
            builder = ServiceProvider.BuildDbContextOptionsBuilder<TDbContext>(builder);

            TDbContext context = (TDbContext)ActivatorUtilities.CreateInstance(ServiceProvider, typeof(TDbContext), builder.Options);
            return context;
        }

        /// <summary>
        /// 创建设计时使用的ServiceProvider，主要用于执行 Add-Migration 功能
        /// </summary>
        /// <returns></returns>
        protected abstract IServiceProvider CreateDesignTimeServiceProvider();
    }
}