using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Shaka.Utils;

namespace Shaka.SQLServerDAL
{
    /// <summary>
    /// 用户（SQL Server数据库实现）
    /// </summary>
    public class User : DALBase, Shaka.IDAL.IUser
    {
        public Shaka.Model.User UserLogin(string loginId, string loginPwd)
        {
            string strSql = @"select * from ShakaUser where userid=@loginId";
            //准备参数
            SqlParameter[] arrSqlParameter = { DBBase.GetPar("@loginId",loginId) };
            List<Shaka.Model.User> UserList = base._dbHelper.Reader<Shaka.Model.User>(strSql, CommandType.Text, arrSqlParameter);
            if (UserList.Count == 1 && UserList[0].UserPwd != CrypHelper.EncryptMD5(loginPwd))
            {
                //用户名对，密码不对
                UserList[0].UserPwd = string.Empty;
            }
            else if (UserList.Count > 1)
            {
                throw new Exception(string.Format("UserId:'{0}'在数据库中存在{1}条", loginId, UserList.Count));
            }
            return UserList[0]; ;
        }
    }
}
