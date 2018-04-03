using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess;
using Utility.HelperCollection.DataAccess.Attribute;

namespace DBDiff.Entity
{
    [SqlTable("dba_tables")]
    public class OracleTableList : EntityBase
    {
        [SqlColumn("OWNER")]
        public virtual string Owner { get; set; }
        [SqlColumn("TABLE_NAME")]
        public virtual string TableName { get; set; }

    }
}
