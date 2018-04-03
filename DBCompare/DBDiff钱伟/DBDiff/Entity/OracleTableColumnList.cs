using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess;
using Utility.HelperCollection.DataAccess.Attribute;

namespace DBDiff.Entity
{
    [SqlTable("dba_tab_columns")]
    public class OracleTableColumnList : EntityBase
    {
        [SqlColumn("OWNER")]
        public virtual string Owner { get; set; }
        [SqlColumn("TABLE_NAME")]
        public virtual string TableName { get; set; }
        [SqlColumn("COLUMN_NAME")]
        public virtual string ColumnName { get; set; }

        [SqlColumn("DATA_TYPE")]
        public virtual string DataType { get; set; }

        [SqlColumn("CHAR_COL_DECL_LENGTH")]
        public virtual int? DataLength { get; set; }

        [SqlColumn("NULLABLE")]
        public virtual string Nullable { get; set; }
        [SqlColumn("DATA_PRECISION")]
        public virtual int? DataPrecision { get; set; }

        [SqlColumn("DATA_SCALE")]
        public virtual int? DataScale { get; set; }

    }
}
