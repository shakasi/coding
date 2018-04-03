using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Shaka.Domain;
using Shaka.Infrastructure;

namespace Shaka.DAL
{
    public class IconOperateBKDAL: BaseDAL
    {
        public bool SaveIcon(List<IconBKInfo> iconList,string strSql)
        {
            SqlDBHelper sqlHelper = new SqlDBHelper();
            sqlHelper.BeginTran();
            try
            {
                sqlHelper.InsertOrUpdateTList<IconBKInfo>(iconList);
                sqlHelper.ExecuteNonQuery(strSql);
                sqlHelper.CommitTran();
                //sqlHelper.RollBackTran(); /////////////////////////////////////////////////////////////////////////////////
                return true;
            }
            catch (Exception ex)
            {
                sqlHelper.RollBackTran();
                throw ex;
            }
        }
    }
}
