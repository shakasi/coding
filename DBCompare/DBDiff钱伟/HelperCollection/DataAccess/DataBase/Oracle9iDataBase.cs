﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Utility.HelperCollection.DataAccess.DataBase
{
    public class Oracle9iDataBase:IDataBase
    {
        public System.Data.Common.DbCommand CreateCommand()
        {
            return new OracleCommand();
        }

        public System.Data.Common.DbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        public System.Data.Common.DbDataAdapter CreateDataAdapter()
        {
            return new OracleDataAdapter();
        }

        public System.Data.Common.DbParameter CreateParameter()
        {
            return new OracleParameter();
        }

        public string DbParameterPrefix
        {
            get { return ":"; }
        }
    }
}
