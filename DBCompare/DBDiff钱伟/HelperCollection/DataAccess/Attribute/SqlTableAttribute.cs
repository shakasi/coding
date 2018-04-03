using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlTableAttribute: System.Attribute
    {
        private string _tableName;
        public SqlTableAttribute(string tableName)
        {
            _tableName = tableName;
        }
        public string TableName { get { return _tableName; } }
    }
}
