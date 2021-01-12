using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
//using VNext.EventBuses;
using VNext.Extensions;
using VNext.Finders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingleton<T>(this IServiceCollection services)
        {
            var instance = services.GetSingletonOrDefault<T>();
            if (instance == null)
            {
                throw new InvalidOperationException($"无法找到已注册的单例服务：{typeof(T).AssemblyQualifiedName}");
            }

            return instance;
        }

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingletonOrDefault<T>(this IServiceCollection services)
        {
            ServiceDescriptor descriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);

            if (descriptor?.ImplementationInstance != null)
            {
                return (T)descriptor.ImplementationInstance;
            }

            if (descriptor?.ImplementationFactory != null)
            {
                return (T)descriptor.ImplementationFactory.Invoke(null);
            }

            return default;
        }

        /// <summary>
        /// 如果指定服务不存在，创建实例并添加
        /// </summary>
        public static TServiceType GetOrAddSingleton<TServiceType>(this IServiceCollection services, Func<TServiceType> factory) where TServiceType : class
        {
            TServiceType item = GetSingletonOrDefault<TServiceType>(services);
            if (item == null)
            {
                item = factory();
                services.AddSingleton<TServiceType>(item);
                //services.LogService(nameof(ServiceExtensions), typeof(TServiceType), item.GetType());
            }
            return item;
        }

        /// <summary>
        /// 如果指定服务不存在，添加指定服务,并返回
        /// </summary>
        public static ServiceDescriptor GetOrAddServiceDescriptor(this IServiceCollection services, ServiceDescriptor toAdDescriptor)
        {
            ServiceDescriptor descriptor = services.FirstOrDefault(m => m.ServiceType == toAdDescriptor.ServiceType);
            if (descriptor != null)
            {
                return descriptor;
            }

            services.Add(toAdDescriptor);
            return toAdDescriptor;
        }

        /// <summary>
        /// 获取或添加指定类型查找器
        /// </summary>
        public static TTypeFinder GetOrAddTypeFinder<TTypeFinder>(this IServiceCollection services, Func<IAppAssemblyFinder, TTypeFinder> factory)
            where TTypeFinder : class
        {
            return services.GetOrAddSingleton<TTypeFinder>(() =>
            {
                IAppAssemblyFinder allAssemblyFinder =
                        services.GetSingleton<IAppAssemblyFinder>();
                return factory(allAssemblyFinder);
            });
        }

        /// <summary>
        /// 加载事件处理器
        /// </summary>
        public static IServiceCollection AddEventHandler<T>(this IServiceCollection services) where T : class, IEventHandler
        {
            return services.AddTransient<T>();
        }

        /// <summary>
        /// 判断指定服务类型是否存在
        /// </summary>
        public static bool AnyServiceType(this IServiceCollection services, Type serviceType)
        {
            return services.Any(m => m.ServiceType == serviceType);
        }

        /// <summary>
        /// 替换服务
        /// </summary>
        public static IServiceCollection Replace<TService, TImplement>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplement), lifetime);
            services.Replace(descriptor);
            return services;
        }

        /// <summary>
        /// 从服务提供者中实现T接口并 同时为Type的子类
        /// </summary>
        public static T GetService<T>(this IServiceProvider provider, Type type)
        {
            var lstT = provider.GetServices<T>();
            if (lstT != null && lstT.Count() > 0)
            {
                return lstT.FirstOrDefault(a => a.GetType().IsBaseOn(type));
            }
            else
            {
                return default;
            }
        }
    }
}