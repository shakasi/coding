using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using log4net;
using Shaka.Model;
using Shaka.BLL;
using Shaka.Utils;

namespace YeZiLiu.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.
            MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Shaka.Model.User user, string returnUrl)
        {
            //测试
            user.UserId ="admin";
            user.UserPwd = "admin";

            //1、界面值验证验证
            if (user.UserId == null || user.UserId.Trim().Length < 4)
            {
                //用户名不可以少于4位

            }
            if (user.UserPwd == null || user.UserPwd.Trim().Length < 6)
            {
                //密码不可以少于6位

            }
            //2、用户名错误还是密码错误
            Shaka.BLL.User userBLL = new Shaka.BLL.User();
            Shaka.Model.User userDB = null;
            try
            {
                userDB = userBLL.GetUser(user.UserId, user.UserPwd);
            }
            catch (Exception ex)
            {
                //1、日志记录错误
                _log.Fatal(ex);
                //2、界面弹出错误

                //3、返回信息
                return View(user);
            }
            if (userDB == null)
            {
                //用户ID不存在
                return View(user);
            }
            else if (string.IsNullOrEmpty(userDB.UserPwd))
            {
                //用户PWD错误
                return View(user);
            }
            //单点登陆
            if (IsLogin(userDB.UserId, userDB.UserName))
            {

            }
            return RedirectToLocal(returnUrl);
        }

        /// <summary>
        /// 验证码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult IdentifyingCode()
        {
            IdentifyingCodeHelper identifyingCodeHelper = new IdentifyingCodeHelper();
            HttpCookie cookie = new HttpCookie("IdentifyingCode");
            cookie.Value = identifyingCodeHelper.CreateRandomCode(4);
            Response.Cookies.Add(cookie);
            MemoryStream ms = identifyingCodeHelper.CreateImage(cookie.Value);
            Response.ClearContent();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.ToArray());
            return View();
        }

        #region 帮助程序
        /// <summary>
        /// 单点登录实现，可以放在05-Infrastructure中，进行中…………………………………………………………
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsLogin(string userId, string userName)
        {
            string existUser = Convert.ToString(HttpRuntime.Cache[userId]);
            if (string.IsNullOrEmpty(existUser))
            {
                #region 缓存依赖项
                string loginUserId = "loginUserId";
                Session[userId] = userId;
                HttpRuntime.Cache.Insert(loginUserId, Session[userId]);
                System.Web.Caching.CacheDependency dep = new System.Web.Caching
                    .CacheDependency(null, new string[] { loginUserId });
                #endregion

                HttpRuntime.Cache.Insert(userId, userName, dep);
                return false;
            }
            return true;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
