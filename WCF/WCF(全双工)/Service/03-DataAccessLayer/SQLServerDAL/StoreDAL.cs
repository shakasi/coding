using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cuscapi.Utils;

namespace Cuscapi.SQLServerDAL
{
    public class StoreDAL : DALBase, Cuscapi.IDAL.IStore
    {
        public List<Cuscapi.Model.StoreInfo> GetStoreList(string group,string type,string value)
        {
            string strSql = @"select * from [dbo].[storeno] where 1=1 and strgroup=@strgroup ";

            if (type == "1") strSql += @" and number like '%' + @value + '%'  "; else strSql += @" and name like '%' + @value + '%'";

            SqlParameter[] arrSqlParameter = {
                SqlDBHelper.GetPar("@strgroup",group,SqlDbType.NVarChar,50,ParameterDirection.Input),
                SqlDBHelper.GetPar("@value",value,SqlDbType.NVarChar,100,ParameterDirection.Input),
            };
            List<Cuscapi.Model.StoreInfo> GroupList = base._dbHelper.Reader<Cuscapi.Model.StoreInfo>(strSql, CommandType.Text, arrSqlParameter);
            return GroupList;
        }
    }
}
