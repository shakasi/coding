using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication1
{
    /// <summary>
    /// 继承 AuthorizeAttribute，就可以达到AOP的目的
    /// </summary>
    public class JWTAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext context)
        {

        }
    }
}