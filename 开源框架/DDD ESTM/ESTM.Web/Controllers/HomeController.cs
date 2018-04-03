using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESTM.Common.DtoModel;
using ESTM.Utility;
using System.ComponentModel.Composition;
using ESTM.Web.IBLL;
using ESTM.Infrastructure.MEF;

namespace ESTM.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Import]
        private IPowerManager PowerManager { set; get; }

        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public JsonResult GetFirstPassReportList(int rows, int page)
        {
            //var lstUsers = new List<DTO_TB_USERS>();
            //for (var i = 0; i < 100; i++)
            //{
            //    var oUser = new DTO_TB_USERS();

            //    oUser.USER_NAME = "aaa" + i;
            //    oUser.USER_PASSWORD = "mamimamihong" + i;
            //    oUser.FULLNAME = "fullname" + i;
            //    lstUsers.Add(oUser);
            //}

            var lstUsers = PowerManager.GetUsers();


            var oRes=Json(new
            {
                total = Convert.ToInt32(Math.Ceiling(lstUsers.Count/(double)rows)),
                page = page,
                records = lstUsers.Count,
                rows = lstUsers.Skip(rows*page-rows).Take(rows).ToArray()
            }, JsonRequestBehavior.AllowGet);
            return oRes;

        }
    }
}