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
    public class HQIconOperateDAL
    {
        private SqlDBHelper _sqlHelper = null;

        public HQIconOperateDAL()
        {
            _sqlHelper = new SqlDBHelper();
        }

        public List<StoreGroupInfo> GetStoreGroupByUser(string loginID, int pageIndex, string sortExpression, int pageSize, ref int totalRow)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(SqlDBHelper.GetPar("loginID", SqlDbType.NVarChar, loginID));
            pars.Add(SqlDBHelper.GetPar("pageIndex", SqlDbType.Int, pageIndex));
            pars.Add(SqlDBHelper.GetPar("sortExpression", SqlDbType.NVarChar, sortExpression));
            pars.Add(SqlDBHelper.GetPar("pageSize", SqlDbType.Int, pageSize));
            pars.Add(SqlDBHelper.GetPar("totalRow", SqlDbType.Int, totalRow, 0, ParameterDirection.Output));

            List<StoreGroupInfo> storeGroupList = _sqlHelper.Reader<StoreGroupInfo>("SP_GetStoreGroupByUser",
                CommandType.StoredProcedure, pars.ToArray());

            var outValue = pars[pars.Count - 1].Value;
            totalRow = outValue == DBNull.Value ? 0 : (int)outValue;

            return storeGroupList;
        }

        public List<HQIconInfo> GetIcon(string selectedFields, string tvName, string whereStatement,
            string sortExpression, int pageIndex, int pageSize, string sgType, string sgNumbers, string effectiveDate, ref int totalRow)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(SqlDBHelper.GetPar("selectedFields", SqlDbType.NVarChar, selectedFields));
            pars.Add(SqlDBHelper.GetPar("TVName", SqlDbType.NVarChar, tvName));
            pars.Add(SqlDBHelper.GetPar("whereStatement", SqlDbType.NVarChar, whereStatement));
            pars.Add(SqlDBHelper.GetPar("sortExpression", SqlDbType.VarChar, sortExpression));
            pars.Add(SqlDBHelper.GetPar("pageIndex", SqlDbType.Int, pageIndex));
            pars.Add(SqlDBHelper.GetPar("pageSize", SqlDbType.Int, pageSize));
            pars.Add(SqlDBHelper.GetPar("SGType", SqlDbType.NVarChar, sgType));
            pars.Add(SqlDBHelper.GetPar("SGNumbers", SqlDbType.NVarChar, sgNumbers));
            pars.Add(SqlDBHelper.GetPar("effectiveDate", SqlDbType.VarChar, effectiveDate));
            pars.Add(SqlDBHelper.GetPar("totalRow", SqlDbType.Int, totalRow, 0, ParameterDirection.Output));

            List<HQIconInfo> iconList = _sqlHelper.Reader<HQIconInfo>("SP_GetItems_SGM_SGE",
                CommandType.StoredProcedure, pars.ToArray());

            var outValue = pars[pars.Count - 1].Value;
            totalRow = outValue == DBNull.Value ? 0 : (int)outValue;

            return iconList;
        }

        public ResultInfo SaveIcon(List<HQSadDTICONInfo> sadDTIconList)
        {
            _sqlHelper.BeginTran();
            try
            {
                foreach (HQSadDTICONInfo sadDTIcon in sadDTIconList)
                {
                    List<SqlParameter> pars = new List<SqlParameter>();
                    pars.Add(SqlDBHelper.GetPar("Storenum", SqlDbType.NVarChar, sadDTIcon.Storenum));
                    pars.Add(SqlDBHelper.GetPar("IconNumber", SqlDbType.NVarChar, sadDTIcon.IconNumber));
                    pars.Add(SqlDBHelper.GetPar("effective_Date", SqlDbType.DateTime, sadDTIcon.effective_Date));
                    pars.Add(SqlDBHelper.GetPar("strgroup", SqlDbType.Int, sadDTIcon.strgroup));
                    pars.Add(SqlDBHelper.GetPar("upd_seq", SqlDbType.Int, sadDTIcon.upd_seq));
                    pars.Add(SqlDBHelper.GetPar("IconName", SqlDbType.NVarChar, sadDTIcon.IconName));
                    pars.Add(SqlDBHelper.GetPar("Status", SqlDbType.Bit, sadDTIcon.Status));
                    pars.Add(SqlDBHelper.GetPar("UpUser", SqlDbType.Int, sadDTIcon.UpUser));
                    pars.Add(SqlDBHelper.GetPar("UpDT", SqlDbType.DateTime, sadDTIcon.UpDT));
                    pars.Add(SqlDBHelper.GetPar("Editor", SqlDbType.NVarChar, sadDTIcon.Editor));
                    pars.Add(SqlDBHelper.GetPar("IconType", SqlDbType.Int, sadDTIcon.IconType));
                    pars.Add(SqlDBHelper.GetPar("deleted", SqlDbType.VarChar, sadDTIcon.deleted));

                    string cmdStr = string.Format(@"
                    DELETE FROM _upd_sadDTICON WHERE Storenum=@Storenum AND IconNumber=@IconNumber AND effective_Date=@effective_Date AND strgroup=@strgroup ;
                    INSERT INTO _upd_sadDTICON(
                    [upd_seq],[Storenum],[IconNumber],[IconName],[Status],[effective_Date],[UpUser],[UpDT],[Editor],[IconType],[strgroup],[deleted]
                    ) 
                    VALUES(
                    @upd_seq,@Storenum,@IconNumber,@IconName,@Status,@effective_Date,@UpUser,@UpDT,@Editor,@IconType,@strgroup,@deleted
                    ) ;"
                        );
                    _sqlHelper.ExecuteNonQuery(cmdStr, pars.ToArray());
                }

                _sqlHelper.CommitTran();
                return ResultInfo.Success;
            }
            catch (Exception ex)
            {
                _sqlHelper.RollBackTran();
                return ResultInfo.Fail;
            }
        }
    }
}