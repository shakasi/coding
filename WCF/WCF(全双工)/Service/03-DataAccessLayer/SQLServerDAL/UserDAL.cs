using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cuscapi.Utils;

namespace Cuscapi.SQLServerDAL
{
    /// <summary>
    /// 用户（SQL Server数据库实现）
    /// </summary>
    public class UserDAL : DALBase, Cuscapi.IDAL.IUser
    {
        public Cuscapi.Model.UserInfo UserLogin(string loginId, string loginPwd)
        {
            string strSql = @"select * from ShakaUser where userid=@loginId";
            //准备参数
            SqlParameter[] arrSqlParameter = { SqlDBHelper.GetPar("@loginId",
                loginId,SqlDbType.NVarChar,50,ParameterDirection.Input) };
            List<Cuscapi.Model.UserInfo> UserList = base._dbHelper.Reader<Cuscapi.Model.UserInfo>(strSql, CommandType.Text, arrSqlParameter);
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
