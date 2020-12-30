using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VNext.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑
        /// </summary>
        public static void ExecuteScopedWork(this IServiceProvider provider, Action<IServiceProvider> action)
        {
            using IServiceScope scope = provider.CreateScope();
            action(scope.ServiceProvider);
        }

        /// <summary>
        /// 异步执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑
        /// </summary>
        public static async Task ExecuteScopedWorkAsync(this IServiceProvider provider, Func<IServiceProvider, Task> action)
        {
            using IServiceScope scope = provider.CreateScope();
            await action(scope.ServiceProvider);
        }

        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑，并获取返回值
        /// </summary>
        public static TResult ExecuteScopedWork<TResult>(this IServiceProvider provider, Func<IServiceProvider, TResult> func)
        {
            using IServiceScope scope = provider.CreateScope();
            return func(scope.ServiceProvider);
        }

        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑，并获取返回值
        /// </summary>
        public static async Task<TResult> ExecuteScopedWorkAsync<TResult>(this IServiceProvider provider, Func<IServiceProvider, Task<TResult>> func)
        {
            using IServiceScope scope = provider.CreateScope();
            return await func(scope.ServiceProvider);
        }
    }
}