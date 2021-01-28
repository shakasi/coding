using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Dependency;
using VNext.Entity;
using VNext.EventBuses;
using VNext.Extensions;
using VNext.Finders;
using VNext.Options;
using VNext.Packs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 项目构建,添加核心模块
        /// </summary>
        public static void AddCorePacks(this IPackBuilder builder)
        {
            builder.AddPack<VNextPack>()
                   .AddPack<DependencyPack>()
                   .AddPack<EventBusPack>();
        }

        /// <summary>
        /// 创建VNext构建器，开始构建VNext服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="tryAddConfiguration">应用配置信息</param>
        /// <param name="startsWith">程序集名称过滤</param>
        /// <returns></returns>
        public static IPackBuilder AddVNext(this IServiceCollection services, IConfiguration tryAddConfiguration = null, params string[] startsWith)
        {
            Check.NotNull(services, nameof(services));

            //初始化系统启动日志
            services.GetOrAddSingleton(() => new StartupLogger());

            //初始化所有程序集查找器
            services.GetOrAddSingleton<IAppAssemblyFinder>(() => new AppAssemblyFinder(startsWith));

            #region 尝试添加wpf应用-缺少的配置信息

            //初始化或新增日志工厂
            services.TryAddLoggerFactory();

            //初始化或新增应用配置信息
            if (tryAddConfiguration != null)
            {
                services.GetOrAddSingleton<IConfiguration>(() => tryAddConfiguration);
            }

            #endregion

            IPackBuilder packBuilder = services.GetOrAddSingleton<IPackBuilder>(() => new PackBuilder(services));
            packBuilder.AddCorePacks();

            return packBuilder;
        }

        /// <summary>
        /// VNext框架初始化，适用于非AspNetCore环境
        /// </summary>
        public static IServiceProvider UseVNext(this IServiceProvider provider)
        {
            ILogger logger = provider.GetLogger(typeof(ServiceExtensions));
            logger.LogInformation("非AspNetCore环境下,框架初始化开始");

            // 输出注入服务的日志
            StartupLogger startupLogger = provider.GetService<StartupLogger>();
            startupLogger.Output(provider);

            Stopwatch watch = Stopwatch.StartNew();
            Pack[] packs = provider.GetServices<Pack>().ToArray();
            foreach (Pack pack in packs)
            {
                pack.UsePack(provider);
                logger.LogInformation($"模块{pack.GetType()}加载成功");
            }

            watch.Stop();
            logger.LogInformation($"框架初始化完毕，耗时：{watch.Elapsed}");

            return provider;
        }

        /// <summary>
        /// 从Scoped字典中获取指定类型的值
        /// </summary>
        public static T GetValue<T>(this ScopedDictionary dict, string key) where T : class
        {
            if (dict.TryGetValue(key, out object obj))
            {
                return obj as T;
            }

            return default(T);
        }

        /// <summary>
        /// 获取指定实体类的上下文所在工作单元
        /// </summary>
        public static IUnitOfWork GetUnitOfWork<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        {
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            return unitOfWorkManager.GetUnitOfWork<TEntity, TKey>();
        }

        /// <summary>
        /// 获取指定实体类型的上下文对象
        /// </summary>
        public static IDbContext GetDbContext<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        {
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            return unitOfWorkManager.GetDbContext<TEntity, TKey>();
        }

        /// <summary>
        /// 获取所有模块信息
        /// </summary>
        public static Pack[] GetAllPacks(this IServiceProvider provider)
        {
            Pack[] packs = provider.GetServices<Pack>().OrderBy(m => m.Level).ThenBy(m => m.Order).ThenBy(m => m.GetType().FullName).ToArray();
            return packs;
        }

        /// <summary>
        /// 开启一个事务处理
        /// </summary>
        /// <param name="provider">信赖注入服务提供程序</param>
        /// <param name="action">要执行的业务委托</param>
        public static void BeginUnitOfWorkTransaction(this IServiceProvider provider, Action<IServiceProvider> action)
        {
            Check.NotNull(provider, nameof(provider));
            Check.NotNull(action, nameof(action));

            using IServiceScope scope = provider.CreateScope();
            IServiceProvider scopeProvider = scope.ServiceProvider;
            IUnitOfWorkManager unitOfWorkManager = scopeProvider.GetService<IUnitOfWorkManager>();
            action(scopeProvider);
            unitOfWorkManager.Commit();
        }

        /// <summary>
        /// 开启一个事务处理
        /// </summary>
        /// <param name="provider">信赖注入服务提供程序</param>
        /// <param name="actionAsync">要执行的业务委托</param>
        public static async Task BeginUnitOfWorkTransactionAsync(this IServiceProvider provider,
            Func<IServiceProvider, Task> actionAsync)
        {
            Check.NotNull(provider, nameof(provider));
            Check.NotNull(actionAsync, nameof(actionAsync));

            using IServiceScope scope = provider.CreateScope();
            IServiceProvider scopeProvider = scope.ServiceProvider;

            IUnitOfWorkManager unitOfWorkManager = scopeProvider.GetService<IUnitOfWorkManager>();
            await actionAsync(scopeProvider);
            unitOfWorkManager.Commit();
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        public static ClaimsPrincipal GetCurrentUser(this IServiceProvider provider)
        {
            try
            {
                IPrincipal user = provider.GetService<IPrincipal>();
                return user as ClaimsPrincipal;
            }
            catch
            {
                return null;
            }
        }
    }
}