using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Shaka.Model;
using Shaka.DALFactory;
using Shaka.IDAL;

namespace Shaka.BLL
{
    /// <summary>
    /// 用户（BLL）
    /// </summary>
    public class User
    {
        private IUser _userDAL = Shaka.DALFactory.Factory.GetUserDAL();

        public Shaka.Model.User GetUser(string loginId, string loginPwd)
        {
            return _userDAL.UserLogin(loginId, loginPwd);
        }
    }
}
