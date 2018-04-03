using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MvcApplication2.Common;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            //HttpException ex = new HttpException(500, "我出错了");
            //throw ex;
        }

        [HandleException]
        public ActionResult Index()
        {
            //int a = 1;
            //int b = 0;
            //var c = a / b;
            return View();
        }

        [HandleException]
        public JsonResult TestAjax()
        {
            List<string> conList = new List<string>();
            conList.Add("中国");
            conList.Add("美国");

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string str = jss.Serialize(conList);
            return Json(str);
        }
    }
}