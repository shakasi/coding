using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.Model.Attr
{
    public class SqlColumnAttribute : Attribute
    {
        private string _columnName;
        public SqlColumnAttribute(string columnName)
        {
            this._columnName = columnName;
        }
        public string ColumnName
        {
            get { return this._columnName; }
        }

        public string DiffReason { get; set; }
    }
}