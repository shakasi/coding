using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VNext.Data;
using VNext.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 从IServiceCollection中,新增<see cref="ILoggerFactory"/>配置信息
        /// </summary>
        public static void TryAddLoggerFactory(this IServiceCollection services)
        {
            if (!services.AnyServiceType(typeof(ILoggerFactory)))
            {
                services.AddLogging(opts =>
                {
#if DEBUG
                    opts.SetMinimumLevel(LogLevel.Debug);
#else
                    opts.SetMinimumLevel(LogLevel.Information);
#endif
                });
                services.LogService<ILoggerFactory, LoggerFactory>(nameof(ServiceExtensions));
            }
        }

        #region ILogger

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <typeparam name="T">非静态强类型</typeparam>
        /// <returns>日志对象</returns>
        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type">指定类型</param>
        /// <returns>日志对象</returns>
        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        /// <summary>
        /// 获取指定对象类型的日志对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="instance">要获取日志的类型对象，一般指当前类，即this</param>
        public static ILogger GetLogger(this IServiceProvider provider, object instance)
        {
            Check.NotNull(instance, nameof(instance));
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(instance.GetType());
        }

        /// <summary>
        /// 获取指定名称的日志对象
        /// </summary>
        public static ILogger GetLogger(this IServiceProvider provider, string name)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(name);
        }

        #endregion

        #region StartupLogger

        /// <summary>
        /// 添加服务调试日志
        /// </summary>
        public static IServiceCollection LogServices(this IServiceCollection services, string logName, ServiceDescriptor[] oldDescriptors)
        {
            var list = services.Except(oldDescriptors);
            foreach (ServiceDescriptor desc in list)
            {
                if (desc.ImplementationType != null)
                {
                    services.LogService(logName, desc.ServiceType, desc.ImplementationType, desc.Lifetime);
                    continue;
                }

                if (desc.ImplementationInstance != null)
                {
                    services.LogService(logName, desc.ServiceType, desc.ImplementationInstance.GetType(), desc.Lifetime);
                }
            }

            return services;
        }

        /// <summary>
        /// 添加服务调试日志
        /// </summary>
        public static IServiceCollection LogService<TServiceType, TImplementType>(this IServiceCollection services, string logName, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            Type serviceType = typeof(TServiceType), implementType = typeof(TImplementType);
            return services.LogService(logName, serviceType, implementType, lifetime);
        }

        /// <summary>
        /// 添加服务调试日志
        /// </summary>
        public static IServiceCollection LogService(this IServiceCollection services, string logName,
            Type serviceType, Type implementType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            return services.LogInfo(logName, $"添加服务，{lifetime.ToString()}，{serviceType.FullName} -> {implementType.FullName}");
        }

        /// <summary>
        /// 添加启动调试日志
        /// </summary>
        public static IServiceCollection LogDebug(this IServiceCollection services, string logName, string message)
        {
            StartupLogger logger = services.GetSingleton<StartupLogger>();
            logger.LogDebug(logName, message);
            return services;
        }

        /// <summary>
        /// 添加启动消息日志
        /// </summary>
        public static IServiceCollection LogInfo(this IServiceCollection services, string logName, string message)
        {
            StartupLogger logger = services.GetSingleton<StartupLogger>();
            logger.LogInfo(logName, message);
            return services;
        }

        /// <summary>
        /// 添加启动消息错误日志
        /// </summary>
        public static IServiceCollection LogError(this IServiceCollection services, string logName, string message, Exception exception)
        {
            StartupLogger logger = services.GetSingleton<StartupLogger>();
            logger.LogError(logName, message, exception);
            return services;
        }

        #endregion        
    }
}