using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class POSSyncInfoLog
    {
        public int ID { get; set; }
        public string storenum { get; set; }
        public int CheckId { get; set; }
        public string MonitorIds { get; set; }
        public DateTime? SyncData { get; set; }
        public bool Status { get; set; }
        public string SyncMes { get; set; }
        public int SyncCode { get; set; }
        public int StepId { get; set; }
        public bool IsDeal { get; set; }
        public DateTime? DealDate { get; set; }
    }
}