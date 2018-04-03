using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.Domain
{
    public class HQSadDTICONInfo
    {
        public int upd_seq { get; set; }

        public string Storenum { get; set; }

        public int IconNumber { get; set; }

        public string IconName { get; set; }

        public bool Status { get; set; }

        public DateTime effective_Date { get; set; }

        public int UpUser { get; set; }

        public DateTime UpDT { get; set; }

        public string Editor { get; set; }

        public int IconType { get; set; }

        public int strgroup { get; set; }

        public bool deleted { get; set; }
    }
}
