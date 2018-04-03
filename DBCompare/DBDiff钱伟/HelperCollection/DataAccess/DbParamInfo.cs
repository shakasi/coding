using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess
{
    public class DbParamInfo
    {
            public string Name { get; set; }
            public object Value { get; set; }
            public ParameterDirection ParameterDirection { get; set; }
            public DbType? DbType { get; set; }
            public int? Size { get; set; }
            public IDbDataParameter AttachedParam { get; set; }
    }
}
