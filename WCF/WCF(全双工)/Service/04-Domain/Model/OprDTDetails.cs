using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class OprDTDetails
    {
        public string storenum { get; set; }
        public DateTime? busidate { get; set; }
        public int dtl_seq { get; set; }
        public int trans_seq { get; set; }
        public int chk_seq { get; set; }
        public int dtl_number { get; set; }
        public int parent_dtl_seq { get; set; }
        public string dtl_type { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int sales_type_seq { get; set; }
        public string sales_type_name { get; set; }
        public bool voided { get; set; }
        public bool error_correct { get; set; }
        public bool mi_return { get; set; }
        public int reason_seq { get; set; }
        public int seat { get; set; }
        public DateTime? entrydate { get; set; }
        public string tdef { get; set; }
        public int chk_cnt { get; set; }
        public decimal chk_ttl { get; set; }
        public int rpt_cnt { get; set; }
        public decimal rpt_ttl { get; set; }
        public string TaxEnable { get; set; }
        public string ServchgTaxEnable { get; set; }
        public int shared_numerator { get; set; }
        public int shared_denominator { get; set; }
        public string remarks { get; set; }
        public decimal percentage { get; set; }
        public string itemizer { get; set; }
        public int mi_menu_itemizer { get; set; }
        public int mi_disc_itemizer { get; set; }
        public int mi_serv_itemizer { get; set; }
        public decimal mi_item_weight { get; set; }
        public int mi_mg { get; set; }
        public string mi_mg_name { get; set; }
        public int mi_fg { get; set; }
        public string mi_fg_name { get; set; }
        public int mi_rg { get; set; }
        public string mi_rg_name { get; set; }
        public int mi_rfactor { get; set; }
        public int MiComboGrp { get; set; }
        public decimal MiPrice { get; set; }
        public decimal tm_tips { get; set; }
        public int tm_currency_seq { get; set; }
        public decimal tm_currency_ttl { get; set; }
        public int tm_ocent { get; set; }
        public int cc_driver_seq { get; set; }
        public int opr_seq { get; set; }
        public int void_dtl_seq { get; set; }
        public decimal AutoServChg { get; set; }
        public decimal AutoServChg_Perct { get; set; }
        public decimal ExemptAutoServChg { get; set; }
        public int CategoryNumber { get; set; }
        public string PromotionKey { get; set; }
        public int DiscountNumber { get; set; }
        public int Avatar { get; set; }
        public bool IsCombo { get; set; }
        public int OrderType { get; set; }
    }
}