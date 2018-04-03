using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.Domain
{
    public class IconInfo
    {
        public int ID { get; set; }

        public int IconNumber { get; set; }

        public int IconType { get; set; }

        public byte[] IconImage { get; set; }

        public string IconName { get; set; }

        public bool Status { get; set; }

        public int UpUser { get; set; }

        public DateTime UpDT { get; set; }
    }
}
