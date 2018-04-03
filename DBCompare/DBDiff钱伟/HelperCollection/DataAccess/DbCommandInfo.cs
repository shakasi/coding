using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess
{
    public class DbCommandInfo
    {
        public DbCommand Command { get; set; }

        public Dictionary<string, DbParameter> PropertyNameToParamter { get; set; }
    }
}
