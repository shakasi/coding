using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class OprDTChecks
    {
        public string storenum { get; set; }
        public DateTime? busidate { get; set; }
        public int chk_seq { get; set; }
        public string id { get; set; }
        public int chk_num { get; set; }
        public string chk_open { get; set; }
        public DateTime? chk_open_date_time { get; set; }
        public DateTime? chk_clsd_date_time { get; set; }
        public DateTime? tbl_open_date_time { get; set; }
        public DateTime? subttl_date_time { get; set; }
        public int drawer_time { get; set; }
        public int rvc_seq { get; set; }
        public string rvc_name { get; set; }
        public int pc_seq { get; set; }
        public string pc_name { get; set; }
        public int empl_num { get; set; }
        public string empl_name { get; set; }
        public int trans_type_seq { get; set; }
        public string trans_type_name { get; set; }
        public int tbl_seq { get; set; }
        public int grp { get; set; }
        public string tbl_name { get; set; }
        public int section { get; set; }
        public string section_name { get; set; }
        public string Status { get; set; }
        public int last_pc_seq { get; set; }
        public string last_pc_name { get; set; }
        public DateTime? last_svc_time { get; set; }
        public int cov_cnt { get; set; }
        public int xfer_chk_num { get; set; }
        public int chk_prntd_cnt { get; set; }
        public string remark1 { get; set; }
        public string remark2 { get; set; }
        public decimal sub_ttl { get; set; }
        public decimal tax_ttl { get; set; }
        public decimal auto_svc_ttl { get; set; }
        public decimal other_svc_ttl { get; set; }
        public decimal pymnt_ttl { get; set; }
        public decimal amt_due_ttl { get; set; }
        public string seatprinted { get; set; }
        public string seatpaid { get; set; }
        public bool standalone_check { get; set; }
        public bool summ_ttl_prntd { get; set; }
        public bool fast_trans_chk { get; set; }
        public bool chk_cancelled { get; set; }
        public bool chk_edited { get; set; }
        public bool training { get; set; }
        public string vatTaxID { get; set; }
        public string vatTaxRDnumber { get; set; }
        public string vattaxnumber { get; set; }
        public string vatARnumber { get; set; }
        public int ccc_id { get; set; }
        public int custid { get; set; }
        public int riderid { get; set; }
        public string ridername { get; set; }
        public DateTime? outtime { get; set; }
        public DateTime? intime { get; set; }
        public int deltime { get; set; }
        public string undeliveryremark { get; set; }
        public int memberid { get; set; }
        public string membername { get; set; }
        public decimal bpoint { get; set; }
        public string memcard { get; set; }
        public string misc { get; set; }
        public int survey_code { get; set; }
        public string survey_code_generate { get; set; }
        public string survey_prt_str { get; set; }
        public string survey_voucher_code { get; set; }
        public string vattaxDes { get; set; }
        public string vatARDes { get; set; }
        public string CardNumber { get; set; }
        public int upload_status { get; set; }
        public decimal tips { get; set; }
        public System.Guid otherId { get; set; }
    }
}