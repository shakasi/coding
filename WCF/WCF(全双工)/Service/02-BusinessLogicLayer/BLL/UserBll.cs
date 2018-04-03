using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.Model;
using Cuscapi.DALFactory;
using Cuscapi.IDAL;

namespace Cuscapi.BLL
{
    /// <summary>
    /// 用户（BLL）
    /// </summary>
    public class UserBLL
    {
        private IUser _userDAL = Cuscapi.DALFactory.Factory.GetUserDAL();

        public Cuscapi.Model.UserInfo GetUser(string loginId, string loginPwd)
        {
            return _userDAL.UserLogin(loginId, loginPwd);
        }
    }
}
