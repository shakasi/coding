using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Shaka.Infrastructure;

namespace Shaka.DAL
{
    public class BaseDAL
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
                    if (par.ParameterName.ToUpper().Equals("pageCount".ToUpper()))
                    {
                        pageCount = Convert.ToInt32(par.Value);
                    }
                    else if (par.ParameterName.ToUpper().Equals("recordsCount".ToUpper()))
                    {
                        recordsCount = Convert.ToInt32(par.Value);
                    }
                }
            }

            return itemList;
        }
    }
}
