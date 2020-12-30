using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using VNext.Data;
using VNext.Extensions;
using VNext.Packs;

namespace VNext.AspNetCore
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/>辅助扩展方法
    /// </summary>
    public static class AspNetCoreExtensions
    {
        /// <summary>
        /// VNext框架初始化，适用于AspNetCore环境
        /// </summary>
        public static IApplicationBuilder UseVNext(this IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            ILogger logger = provider.GetLogger(typeof(AspNetCoreExtensions));
            logger.LogInformation(0, "AspNetCore环境下,框架初始化开始");

            // 输出注入服务的日志
            StartupLogger startupLogger = provider.GetService<StartupLogger>();
            startupLogger.Output(provider);

            Stopwatch watch = Stopwatch.StartNew();
            Pack[] packs = provider.GetAllPacks();
            logger.LogInformation($"共有 {packs.Length} 个Pack模块需要初始化");
            foreach (Pack pack in packs)
            {
                Type packType = pack.GetType();
                string packName = packType.GetDescription();
                logger.LogInformation($"正在初始化模块 “{packName} ({packType.Name})”");
                if (pack is AspPack aspPack)
                {
                    aspPack.UsePack(app);
                }
                else
                {
                    pack.UsePack(provider);
                }
                logger.LogInformation($"模块 “{packName} ({packType.Name})” 初始化完成\n");
            }

            watch.Stop();
            logger.LogInformation(0, $"框架初始化完成，耗时：{watch.Elapsed}\r\n");

            return app;
        }

        /// <summary>
        /// 添加Endpoint并Area路由支持
        /// </summary>
        public static IEndpointRouteBuilder MapControllersWithAreaRoute(this IEndpointRouteBuilder endpoints, bool area = true)
        {
            if (area)
            {
                endpoints.MapControllerRoute(
                    name: "areas-router",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return endpoints;
        }

        #region HttpRequest
        /// <returns>
        /// 如果指定的 HTTP 请求是 AJAX 请求，则为 true；否则为 false。
        /// </returns>
        /// <param name="request">HTTP 请求。</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="request"/> 参数为 null（在 Visual Basic 中为 Nothing）。</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            Check.NotNull(request, nameof(request));

            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal)
                || string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        /// <summary>
        /// 确定指定的 HTTP 请求的 ContextType 是否为 Json 方式
        /// </summary>
        public static bool IsJsonContextType(this HttpRequest request)
        {
            Check.NotNull(request, nameof(request));
            bool flag = request.Headers?["Content-Type"].ToString().IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                || request.Headers?["Content-Type"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
            if (flag)
            {
                return true;
            }
            flag = request.Headers?["Accept"].ToString().IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                || request.Headers?["Accept"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
            return flag;
        }

        /// <summary>
        /// 获取<see cref="HttpRequest"/>的请求数据
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="key">要获取数据的键名</param>
        /// <returns></returns>
        public static string Params(this HttpRequest request, string key)
        {
            if (request.Query.ContainsKey(key))
            {
                return request.Query[key];
            }
            if (request.HasFormContentType)
            {
                return request.Form[key];
            }
            return null;
        }

        #endregion

        #region HttpContext
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        public static string GetClientIp(this HttpContext context)
        {
            string ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        #endregion
    }
}