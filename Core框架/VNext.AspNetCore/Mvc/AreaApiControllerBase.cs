﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using VNext.AspNetCore.Mvc.Filters;

namespace VNext.AspNetCore.Mvc
{
    /// <summary>
    /// WebApi的区域控制器基类
    /// </summary>
    [AuditOperation]
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    public abstract class AreaApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger => HttpContext.RequestServices.GetLogger(GetType());
    }
}