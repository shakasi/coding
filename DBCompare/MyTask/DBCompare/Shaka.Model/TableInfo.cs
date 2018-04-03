using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shaka.Model.Attr;

namespace Shaka.Model
{
    public class TableInfo
    {
        [SqlColumn("表名")]
        public virtual string TableName { get; set; }

        [SqlColumn("序号")]
        public virtual int SortNum { get; set; }

        [SqlColumn("列名")]
        public virtual string ColumnName { get; set; }

        [SqlColumn("列说明")]
        public virtual string ColumnDescription { get; set; }

        [SqlColumn("数据类型", DiffReason = "数据类型不一致")]
        public virtual string DataType { get; set; }

        [SqlColumn("长度", DiffReason = "长度不一致")]
        public virtual int DataLength { get; set; }

        [SqlColumn("小数位数", DiffReason = "小数位数不一致")]
        public virtual int DecimalDigits { get; set; }

        [SqlColumn("标识", DiffReason = "标识不一致")]
        public virtual bool IsIdentity { get; set; }

        [SqlColumn("主键", DiffReason = "主键不一致")]
        public virtual bool PrimaryKey { get; set; }

        [SqlColumn("允许空", DiffReason = "允许空不一致")]
        public virtual bool NullAble { get; set; }

        [SqlColumn("默认值", DiffReason = "默认值不一致")]
        public virtual string DefaultValue { get; set; }
    }
}
