using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiff
{
    public class TableColumnDiffInfo
    {
        public string TableName { get; set; }

        public string ColumName { get; set; }

        public string DiffReason { get; set; }

       // public string UpdateSql { get; set; }
    }
}
