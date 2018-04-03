using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiff
{
    public class TableDiffInfo
    {
        public string TableName { get; set; }
        public string DiffReason { get; set; }

        public List<string> UpdateSql { get; set; }
    }
}
