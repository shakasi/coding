using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cuscapi.Model;

namespace Cuscapi.IDAL
{
    public interface IWCFDataSync
    {
        void DataSync(DataSet ds);

        void UploadSalesData(DataSet ds);
    }
}
