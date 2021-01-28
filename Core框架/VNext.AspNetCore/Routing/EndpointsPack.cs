using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace VNext.AspNetCore.Routing
{
    /// <summary>
    /// Endpoints模块
    /// </summary>
    [Description("Endpoints模块")]
    public class EndpointsPack : EndpointsPackBase
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            return services;
        }

        /// <summary>
        /// 重写以配置SignalR的终结点
        /// </summary>
        protected override IEndpointRouteBuilder SignalREndpoints(IEndpointRouteBuilder endpoints)
        {
            // 在这实现Hub的路由映射
            // 例如：endpoints.MapHub<ChatHub>();
            return endpoints;
        }

        /// <summary>
        /// 重写以配置其他终结点
        /// </summary>
        protected override IEndpointRouteBuilder OtherEndpoints(IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapRazorPages();
            return endpoints;
        }
    }
}
