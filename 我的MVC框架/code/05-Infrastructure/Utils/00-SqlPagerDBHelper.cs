using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Shaka.Utils
{
    public class SqlPagerDBHelper
    {
        /// <summary>
        /// 调用通用分页存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual List<T> GetItems<T>(ref int pageCount, ref int recordsCount, params SqlParameter[] pars) where T : new()
        {
            SqlDBHelper sqlHelper = new SqlDBHelper();

            string proName = "procSplitPages";
            List<T> itemList = sqlHelper.Reader<T>(proName, CommandType.StoredProcedure, pars);

            foreach (SqlParameter par in pars)
            {
                if (par.Direction == ParameterDirection.Output && par.Value != DBNull.Value)
                {
                    //@pageCount=(@recordsCount/@pageSize+ case count(*)%@pageSize when 0 then 0 else 1 end)

                    if (par.ParameterName.ToUpper().Equals("recordsCount".ToUpper()))
                    {
                        recordsCount = Convert.ToInt32(par.Value);
                        break;
                    }
                }
            }

            return itemList;
        }
    }

    public class SearchParamInfo
    {
        private string _tempTableStr;

        private string _sqlStr;

        private string _whereStr = string.Empty;

        private string _orderStr = string.Empty;

        private int _pageIndex { get; set; }

        private int _pageSize { get; set; }

        public SearchParamInfo(string tempTableStr, string sqlStr, string whereStr, string orderStr,
            int pageIndex, int pageSize)
        {
            this._tempTableStr = tempTableStr;
            this._sqlStr = sqlStr;
            this._whereStr = whereStr;
            this._orderStr = orderStr;
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
        }

        public SqlParameter[] GetPars()
        {
            List<SqlParameter> parList = new List<SqlParameter>();
            parList.Add(DBBase.GetPar("tempTableStr", _tempTableStr));
            parList.Add(DBBase.GetPar("sqlStr", _sqlStr));
            //如果 TempTableStr 不空，那么条件是加到 TempTableStr 的
            parList.Add(DBBase.GetPar("whereStr", _whereStr));
            parList.Add(DBBase.GetPar("orderStr", _orderStr));
            parList.Add(DBBase.GetPar("pageIndex", _pageIndex, SqlDbType.Int));
            parList.Add(DBBase.GetPar("pageSize", _pageSize, SqlDbType.Int));

            int recordsCount = 0;
            parList.Add(DBBase.GetPar("recordsCount", recordsCount, dbType: SqlDbType.Int, dir: ParameterDirection.Output));

            return parList.ToArray();
        }
    }
}
