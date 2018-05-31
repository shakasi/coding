using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/UserInfo")]
    public class ShowController : ApiController
    {
        [JWTAuth]
        [HttpGet, Route()]
        public IHttpActionResult GetUserInfo()
        {
            var person = new List<Person>
            {
                new Person("无聊工作室",23,'x'),
                new Person("无聊工作室",23,'x'),
                new Person("无聊工作室",23,'x')
            };
            return Ok(person);
        }
    }
}