using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Utility.HelperCollection.DataAccess.DataBase
{
    public  class MSSqlserverDataBase:IDataBase
    {
        public System.Data.Common.DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public System.Data.Common.DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public System.Data.Common.DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public System.Data.Common.DbParameter CreateParameter()
        {
            return new SqlParameter();
        }


        public string DbParameterPrefix
        {
            get { return "@"; }
        }
    }
}
