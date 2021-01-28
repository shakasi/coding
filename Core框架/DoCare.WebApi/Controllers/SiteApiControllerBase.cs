using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using VNext.AspNetCore.Mvc;
using VNext.Authorization;

namespace DoCare.Hosting.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettingsAttribute(GroupName = "docare")]
    public abstract class SiteApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger => HttpContext.RequestServices.GetLogger(GetType());
    }
}