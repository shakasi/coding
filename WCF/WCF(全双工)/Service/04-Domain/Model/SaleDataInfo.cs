using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.Model
{
    public class SaleDataInfo
    {
        public string UniqueCode
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string PassWord
        {
            get;
            set;
        }


        public List<StoreChecksDate> ChecksData
        {
            get;
            set;
        }

    }

    public class StoreChecksDate
    {
        public string StoreCode
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public int CheckCount
        {
            get;
            set;
        }

        public int CustomerTotal
        {
            get;
            set;
        }

        public double ChecksTotal
        {
            get;
            set;
        }

        public string WXSalesDataUniqueCode
        {
            get;
            set;
        }
    }
}