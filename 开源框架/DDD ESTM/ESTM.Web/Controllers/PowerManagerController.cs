using ESTM.Common.DtoModel;
using ESTM.Web.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESTM.Web.Controllers
{
    public class PowerManagerController : BaseController
    {
        [Import]
        private IPowerManager PowerManager { set; get; }

        #region Views
        // GET: PowerManager
        public ActionResult User()
        {
            return View();
        }

        public ActionResult Role()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Department()
        {
            return View();
        } 
        #endregion

        #region 部门管理
        public JsonResult GetDepartments(int limit, int offset, string departmentname, string statu)
        {
            //得到lamada表达式
            var oLamadaExtention = new LamadaExtention<DTO_TB_DEPARTMENT>();
            if (!string.IsNullOrEmpty(departmentname))
            {
                oLamadaExtention.GetExpression("DEPARTMENT_NAME", departmentname, ExpressionType.Contains);
            }
            if (!string.IsNullOrEmpty(statu))
            {
                oLamadaExtention.GetExpression("STATUS", statu, ExpressionType.Contains);
            }
            var lamada = oLamadaExtention.GetLambda();
            var lstRes = PowerManager.GetDepartments(lamada);

            return Json(new { rows = lstRes.Skip(offset).Take(limit).ToList(), total = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public object GetDepartmentEdit(string strPostData)
        {
            var oDepartment = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO_TB_DEPARTMENT>(strPostData);
            if (string.IsNullOrEmpty(oDepartment.DEPARTMENT_ID))
            {
                oDepartment.DEPARTMENT_ID = Guid.NewGuid().ToString();
                oDepartment = PowerManager.AddDepartment(oDepartment);
            }
            else
            {
                PowerManager.UpdateDepartment(oDepartment);
            }
            return oDepartment;
        }

        public object DeleteDept(string strID)
        {
            PowerManager.DeleteDepartment(x=>x.DEPARTMENT_ID == strID);
            return new object();
        }
        #endregion

        #region 菜单管理
        public JsonResult GetMenus(int limit, int offset, string menuname, string menuurl)
        {
            var oLamadaExtention = new LamadaExtention<DTO_TB_MENU>();
            if (!string.IsNullOrEmpty(menuname))
            {
                oLamadaExtention.GetExpression("MENU_NAME", menuname, ExpressionType.Contains);
            }
            if (!string.IsNullOrEmpty(menuurl))
            {
                oLamadaExtention.GetExpression("MENU_URL", menuurl, ExpressionType.Contains);
            }
            var lamada = oLamadaExtention.GetLambda();
            var lstRes = PowerManager.GetMenus(lamada).ToList();

            return Json(new { rows = lstRes.Skip(offset).Take(limit).ToList(), total = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public object GetMenuEdit(string strPostData)
        {
            var oMenu = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO_TB_MENU>(strPostData);
            if (string.IsNullOrEmpty(oMenu.MENU_ID))
            {
                //oMenu = MenuManager.Add(oMenu);
            }
            else
            {
                //MenuManager.Update(oMenu);
            }
            return oMenu;
        }

        public object DeleteMenu(string strID)
        {
            //MenuManager.Delete(strID);
            return new object();
        }

        public object GetParentMenu()
        {
            var lstMenu = PowerManager.GetMenus(x => x.MENU_LEVEL == "1");

            //var lstRes = RoleManager.Find().ToList();
            //var oRes = new PageRowData();
            //oRes.rows = lstRes.Skip(offset).Take(limit).ToList();
            //oRes.total = lstRes.Count;
            return lstMenu; ;
        }

        public object GetChildrenMenu(string strParentID)
        {
            var lstMenu = PowerManager.GetMenus(x => x.MENU_LEVEL == "2" && x.PARENT_ID == strParentID).ToList();

            //var lstRes = RoleManager.Find().ToList();
            //var oRes = new PageRowData();
            //oRes.rows = lstRes.Skip(offset).Take(limit).ToList();
            //oRes.total = lstRes.Count;
            return lstMenu; ;
        }
        #endregion

        #region 权限管理

        public JsonResult GetRole(int limit, int offset, string rolename, string desc)
        {
            var oLamadaExtention = new LamadaExtention<DTO_TB_ROLE>();
            if (!string.IsNullOrEmpty(rolename))
            {
                oLamadaExtention.GetExpression("ROLE_NAME", rolename, ExpressionType.Contains);
            }
            if (!string.IsNullOrEmpty(desc))
            {
                oLamadaExtention.GetExpression("DESCRIPTION", desc, ExpressionType.Contains);
            }
            var lamada = oLamadaExtention.GetLambda();
            var lstRes = PowerManager.GetRoles(lamada).ToList();

            return Json(new { rows = lstRes.Skip(offset).Take(limit).ToList(), total = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 用户管理
        public JsonResult GetUsers(int limit, int offset, string username, string fullname)
        {
            var oLamadaExtention = new LamadaExtention<DTO_TB_USERS>();
            if (!string.IsNullOrEmpty(username))
            {
                oLamadaExtention.GetExpression("USER_NAME", username, ExpressionType.Contains);
            }
            if (!string.IsNullOrEmpty(fullname))
            {
                oLamadaExtention.GetExpression("FULLNAME", fullname, ExpressionType.Contains);
            }
            var lamada = oLamadaExtention.GetLambda();

            var lstRes = PowerManager.GetUsers(lamada).ToList();

            return Json(new { rows = lstRes.Skip(offset).Take(limit).ToList(), total = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public object GetUserEdit(string strPostData)
        {
            var oUser = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO_TB_USERS>(strPostData);
            if (string.IsNullOrEmpty(oUser.USER_ID))
            {
                oUser.USER_ID = Guid.NewGuid().ToString();
                oUser = PowerManager.AddUser(oUser);
            }
            else
            {
                PowerManager.UpdateUser(oUser);
            }
            return oUser;
        }

        public object DeleteUser(string strID)
        {
            PowerManager.DeleteUser(x => x.USER_ID == strID);
            return new object();
        }
        #endregion
    }
}