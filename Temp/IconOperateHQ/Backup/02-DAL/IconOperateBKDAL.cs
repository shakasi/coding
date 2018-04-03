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
    public class IconOperateBKDAL
    {
        public List<IconBKInfo> QueryIcon(int iconNumber, string iconName)
        {
            List<IconBKInfo> iconList = new List<IconBKInfo>();
            string sqlStr = @"select top 100 * from sadDTICON where 1=1";
            if (iconNumber!=0)
            {
                sqlStr += string.Format(" and iconNumber like '%'+N'{0}'+'%'", iconNumber);
            }
            if (!"".Equals(iconName))
            {
                sqlStr += string.Format(" and iconName like '%'+N'{0}'+'%'", iconName);
            }
            sqlStr += " order by IconNumber desc";
            SqlDBHelper sqlHelper = new SqlDBHelper();
            iconList = sqlHelper.Reader<IconBKInfo>(sqlStr);
            return iconList;
        }

        public ResultInfo SaveIcon(int iconNumber, string iconName,FileInfo file)
        {
            byte[] bytes = BinaryHelper.FileToBytes(file.FullName);
            if (bytes == null)
            {
                return ResultInfo.Fail;
            }
            SqlDBHelper sqlHelper = new SqlDBHelper();
            sqlHelper.BeginTran();
            try
            {
                string sqlStr = @"update sadDTICON set IconImage=@IconImage where iconNumber=@iconNumber";
                SqlParameter[] pars = GetPars(iconNumber, bytes); 
                int line = sqlHelper.ExecuteNonQuery(sqlStr, pars);
                if(line==1)
                {
                    sqlHelper.CommitTran();
                    return ResultInfo.Cover;
                }
                else if (line > 1)
                {
                    sqlHelper.RollBackTran();
                    return ResultInfo.Fail;
                }
                else
                {
                    //新增
                    if (iconNumber == 0)
                    {
                        iconNumber =Convert.ToInt32( sqlHelper.ExecuteScalar("select max(IconNumber)+1 from sadDTICON"));
                    }
                    if (iconName=="")
                    {
                        iconName = file.Name;
                    }
                    int calc_No = Convert.ToInt32(sqlHelper.ExecuteScalar("select max(Calc_No)+1 from sadDTICON"));
                    sqlStr = string.Format("insert into sadDTICON values ({0},1,@IconImage,'{1}',1,13,GETDATE())",
                        iconNumber, iconName);
                    pars = GetPars(iconNumber, bytes);
                    int lineIns = sqlHelper.ExecuteNonQuery(sqlStr, pars);
                    if (lineIns == 1)
                    {
                        sqlHelper.CommitTran();
                        return ResultInfo.Increase;
                    }
                    else
                    {
                        sqlHelper.RollBackTran();
                        return ResultInfo.Fail;
                    }
                }
            }
            catch(Exception ex)
            {
                sqlHelper.RollBackTran();
                throw ex;
            }       
        }

        private SqlParameter[] GetPars(int iconNumber, byte[] iconImage)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = SqlDBHelper.GetPar("iconNumber", SqlDbType.Int, iconNumber);
            pars[1] = SqlDBHelper.GetPar("IconImage", SqlDbType.Image, iconImage);
            return pars;
        }
    }
}
