using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VNext.AspNetCore.Mvc.Filters;

namespace VNext.AspNetCore.Mvc
{
    /// <summary>
    /// 区域的Mvc控制器基类，配置了操作审计
    /// </summary>
    [AuditOperation]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger => HttpContext.RequestServices.GetLogger(GetType());
    }
}