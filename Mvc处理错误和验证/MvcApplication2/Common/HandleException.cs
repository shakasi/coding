using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Common
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class HandleException : IExceptionFilter
    //{
    //    public void OnException(ExceptionContext filterContext)
    //    {
    //        UrlHelper url = new UrlHelper(filterContext.RequestContext);
    //        filterContext.Result = new RedirectResult(url.Action("Handle", "Exception"));
    //    }
    //}

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandleException : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //UrlHelper url = new UrlHelper(filterContext.RequestContext);
            //filterContext.Result = new RedirectResult(url.Action("Handle", "Exception"));
        }

        // 一般下面用来处理权限
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}
