using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess.Attribute;

namespace Utility.HelperCollection.DataAccess
{
    public class DbColumnInfo
    {
        private DbColumnInfo()
        {

        }
        public string PropertyName { get; private set; }
        public string ColumnName { get; private set; }
        public Type Type { get; private set; }
        public MethodInfo SetMethod { get; private set; }
        public MethodInfo GetMethod { get; private set; }

        public static DbColumnInfo BuildInstanceForPropertyInfo(PropertyInfo pi)
        {
            DbColumnInfo dci = new DbColumnInfo();
            dci.PropertyName = pi.Name;
            dci.SetMethod = pi.SetMethod;
            dci.GetMethod = pi.GetGetMethod();
            dci.Type = pi.PropertyType;
            SqlColumnAttribute colattr = (SqlColumnAttribute)pi.GetCustomAttribute(typeof(SqlColumnAttribute));
            if (colattr != null)
            {
                dci.ColumnName = colattr.ColumnName;
            }
            return dci;
        }

    }
}
