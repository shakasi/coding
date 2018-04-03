using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Cuscapi.SQLServerDAL;

/// <summary>
/// DataSyncBLL 的摘要说明
/// </summary>
public class WCFDataSyncBLL
{
    private WCFDataSyncDAL _dataSyncDAL = null;

    public WCFDataSyncBLL()
    {
        _dataSyncDAL = new WCFDataSyncDAL();
    }

    public void DataSync(DataSet ds)
    {
        _dataSyncDAL.DataSync(ds);
    }
    public void UploadSalesData(DataSet ds)
    {
        _dataSyncDAL.UploadSalesData(ds);
    }
}