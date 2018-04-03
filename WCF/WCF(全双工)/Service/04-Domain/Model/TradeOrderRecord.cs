using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class TradeOrderRecord
    {
        public string GUID { get; set; }
        public string storenum { get; set; }
        public int chk_seq { get; set; }
        public string out_trade_no { get; set; }
        public int trade_status_code { get; set; }
        public string trade_status { get; set; }
        public int plat_type { get; set; }
        public int payment_type { get; set; }
        public int total_amount { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string goods_detail { get; set; }
        public string buyer_id { get; set; }
        public string qr_code { get; set; }
        public DateTime? order_date { get; set; }
        public string remark { get; set; }
        public string operator_id { get; set; }
        public DateTime? send_pay_date { get; set; }
    }
}