using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cuscapi.Utils;
using Cuscapi.Model;

namespace Cuscapi.SQLServerDAL
{
    /// <summary>
    /// 用户（SQL Server数据库实现）
    /// </summary>
    public class GroupDAL : DALBase, Cuscapi.IDAL.IGroup
    {
        public List<GroupInfo> AllGroup()
        {
            string strSql = @"select * from [dbo].[strgroup]";

            List<GroupInfo> GroupList = base._dbHelper.Reader<GroupInfo>(strSql, CommandType.Text, null);

            return GroupList;
        }
    }
}
