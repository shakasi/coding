using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cuscapi.Utils;

namespace Cuscapi.SQLServerDAL
{
    public class WCFDataSyncDAL : DALBase, Cuscapi.IDAL.IWCFDataSync
    {
        public void DataSync(DataSet ds)
        {
            _dbHelper.BeginTran();
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    _dbHelper.InsertOrUpdateTList(dt);
                }
                _dbHelper.CommitTran();
            }
            catch (Exception ex)
            {
                _dbHelper.RollBackTran();
            }
        }

        public void UploadSalesData(DataSet ds)
        {
            _dbHelper.BeginTran();
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    _dbHelper.InsertOrUpdateTList(dt);
                }
                _dbHelper.CommitTran();
            }
            catch (Exception ex)
            {
                _dbHelper.RollBackTran();
            }
        }
    }
}
