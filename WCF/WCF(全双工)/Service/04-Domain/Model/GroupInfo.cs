using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.Model
{
    public class GroupInfo
    {
        public int GroupNumber { get; set; }

        public string GroupName { get; set; }
        public int GroupPos { get; set; }
        public int GroupInventory { get; set; }

        public int GroupMem { get; set; }

        public string AllowImport { get; set; }

        public int ExUpdate { get; set; }

        public int SystemType { get; set; }

        public int UploadStatus { get; set; }
    }
}
