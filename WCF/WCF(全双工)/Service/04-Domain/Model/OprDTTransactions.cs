using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class OprDTTransactions
    {
        public string storenum { get; set; }
        public DateTime? busidate { get; set; }
        public int trans_seq { get; set; }
        public string trans_type { get; set; }
        public int chk_seq { get; set; }
        public int rvc_seq { get; set; }
        public string rvc_name { get; set; }
        public DateTime? business_date { get; set; }
        public int pc_seq { get; set; }
        public string pc_name { get; set; }
        public int SerPeriod_seq { get; set; }
        public string SerPeriod_name { get; set; }
        public string SerPeriod_range { get; set; }
        public int cashier { get; set; }
        public string cashier_name { get; set; }
        public int empl_seq { get; set; }
        public string empl_name { get; set; }
        public bool training { get; set; }
        public int empl_role { get; set; }
        public string empl_role_name { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public bool begin { get; set; }
        public bool paid { get; set; }
        public int cov_cnt { get; set; }
        public string status { get; set; }
        public int trans_type_seq { get; set; }
        public string trans_type_name { get; set; }
        public int prev_trans_type_seq { get; set; }
        public string prev_trans_type_name { get; set; }
        public int cancel_sale_type_seq { get; set; }
        public string cancel_sale_type_name { get; set; }
        public string TaxOptions { get; set; }
        public decimal autosvc { get; set; }
        public decimal AutoSvcRounding { get; set; }
        public decimal TransRounding { get; set; }
        public string remark1 { get; set; }
        public string remark2 { get; set; }
        public DateTime? printdate { get; set; }
        public int tbl_seq { get; set; }
        public string tbl_name { get; set; }
        public int prev_tbl_seq { get; set; }
        public string prev_tbl_name { get; set; }
        public int PrintStatus { get; set; }
        public bool isReady { get; set; }
        public string score { get; set; }
    }
}