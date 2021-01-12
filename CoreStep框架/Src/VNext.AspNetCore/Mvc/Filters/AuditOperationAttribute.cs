﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
//using VNext.Authorization;
//using VNext.Authorization.Functions;
//using VNext.Dependency;
//using VNext.Entity;
//using VNext.Identity;
//using VNext.Systems;

namespace VNext.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 操作审计拦截器，负责发起并记录功能的操作日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuditOperationAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //IServiceProvider provider = context.HttpContext.RequestServices;
            //IFunction function = context.GetExecuteFunction();
            //if (function == null)
            //{
            //    return;
            //}
            //ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            //dict.Function = function;
            //// 数据权限有效角色，即有当前功能权限的角色
            //IFunctionAuthorization functionAuthorization = provider.GetService<IFunctionAuthorization>();
            //string[] roleName = functionAuthorization.GetOkRoles(function, context.HttpContext.User);
            //dict.DataAuthValidRoleNames = roleName;

            //if (!function.AuditOperationEnabled)
            //{
            //    return;
            //}
            //AuditOperationEntry operation = new AuditOperationEntry
            //{
            //    FunctionName = function.Name,
            //    Ip = context.HttpContext.GetClientIp(),
            //    UserAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
            //    CreatedTime = DateTime.Now
            //};
            //if (context.HttpContext.User.Identity.IsAuthenticated && context.HttpContext.User.Identity is ClaimsIdentity identity)
            //{
            //    operation.UserId = identity.GetUserId();
            //    operation.UserName = identity.GetUserName();
            //    operation.NickName = identity.GetNickName();
            //}

            //dict.AuditOperation = operation;
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //IServiceProvider provider = context.HttpContext.RequestServices;
            //ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            //if (dict.AuditOperation?.FunctionName == null)
            //{
            //    return;
            //}
            //dict.AuditOperation.EndedTime = DateTime.Now;
            //IUnitOfWork unitOfWork = provider.GetUnitOfWork<Function, Guid>();
            ////回滚之前业务处理中的未提交事务，防止审计信息保存时误提交
            //unitOfWork?.Rollback();

            ////移除当前功能，使保存审计信息的时候不再获取记录变更，审计信息不需要再审计
            //dict.Function = null;
            //IAuditStore store = provider.GetService<IAuditStore>();
            //store?.Save(dict.AuditOperation);
            //unitOfWork?.Commit();
        }

    }
}

/* Filter执行顺序
ClassFilter - OnActionExecuting
MethodFilter - OnActionExecuting
MethodFilter - OnActionExecuted
ClassFilter - OnActionExecuted
ClassFilter - OnResultExecuting
MethodFilter - OnResultExecuting
MethodFilter - OnResultExecuted
ClassFilter - OnResultExecuted
*/
