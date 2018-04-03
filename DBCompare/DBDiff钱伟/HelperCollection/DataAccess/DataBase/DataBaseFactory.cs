using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess.DataBase
{
    public static class DataBaseFactory
    {
        public static IDataBase CreateDataBase(string provideName)
        {
           // return new MSSqlserverDataBase();
            throw new NotImplementedException();
        }
        public static IDataBase CreateDataBase(DataBaseType db)
        {
            if (db == DataBaseType.MSSQLServer)
            {
                return new MSSqlserverDataBase();
            }
            else if (db == DataBaseType.Oracle10Plus)
            {
                return new OracleDataBase();
            }
            else if (db == DataBaseType.Oracle9i)
            {
                return new Oracle9iDataBase();
            }
            else
            {
                throw new NotSupportedException("不是有效地数据库类型");
            }
        }
    }

}
