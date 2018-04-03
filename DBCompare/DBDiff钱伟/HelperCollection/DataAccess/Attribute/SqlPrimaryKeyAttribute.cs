using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess.Attribute
{
    public class SqlPrimaryKeyAttribute : System.Attribute
    {
        public string[] Key { get; private set; }
        public SqlPrimaryKeyAttribute(params string[] keys)
        {
            Key = keys;
        }

    }
}
