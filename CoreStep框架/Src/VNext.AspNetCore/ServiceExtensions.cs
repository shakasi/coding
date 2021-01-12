using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/>扩展方法
    /// </summary>
    public static class ServiceExtensions
    {
        #region IServiceCollection

        /// <summary>
        /// 获取<see cref="IHostingEnvironment"/>环境信息
        /// </summary>
        public static IWebHostEnvironment GetWebHostEnvironment(this IServiceCollection services)
        {
            return services.GetSingleton<IWebHostEnvironment>();
        }

        #endregion

        #region IServiceProvider

        /// <summary>
        /// 获取HttpContext实例
        /// </summary>
        public static HttpContext HttpContext(this IServiceProvider provider)
        {
            IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
            return accessor?.HttpContext;
        }

        /// <summary>
        /// 当前业务是否处于HttpRequest中
        /// </summary>
        public static bool InHttpRequest(this IServiceProvider provider)
        {
            var context = provider.HttpContext();
            return context != null;
        }

        #endregion
    }
}