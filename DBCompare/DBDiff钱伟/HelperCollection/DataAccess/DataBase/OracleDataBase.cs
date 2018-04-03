using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Utility.HelperCollection.DataAccess.DataBase
{
    public class OracleDataBase:IDataBase
    {
        public System.Data.Common.DbCommand CreateCommand()
        {
            //throw new NotImplementedException();
            return new OracleCommand();
        }

        public System.Data.Common.DbConnection CreateConnection()
        {
            //throw new NotImplementedException();
             
            return new OracleConnection();
        }

        public System.Data.Common.DbDataAdapter CreateDataAdapter()
        {
            //throw new NotImplementedException();
            return new OracleDataAdapter();
        }

        public System.Data.Common.DbParameter CreateParameter()
        {
            //throw new NotImplementedException();
            return new OracleParameter();
        }

        public string DbParameterPrefix
        {
            get { return ":"; }
        }
    }
}
