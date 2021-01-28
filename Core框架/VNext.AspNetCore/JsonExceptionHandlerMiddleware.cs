using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Extensions;

namespace VNext.AspNetCore
{
    /// <summary>
    /// Json技术异常处理中间件
    /// </summary>
    public class JsonExceptionHandlerMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JsonExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// 初始化一个<see cref="JsonExceptionHandlerMiddleware"/>类型的新实例
        /// </summary>
        public JsonExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<JsonExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Text.Json") return;
                _logger.LogError(new EventId(), ex, ex.Message);
                if (context.Request.IsAjaxRequest() || context.Request.IsJsonContextType())
                {
                    if (context.Response.HasStarted)
                    {
                        return;
                    }
                    context.Response.Clear();
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(new AjaxResult(ex.Message, AjaxResultType.Error).ToJsonString());
                    return;
                }
                throw;
            }
        }
    }
}