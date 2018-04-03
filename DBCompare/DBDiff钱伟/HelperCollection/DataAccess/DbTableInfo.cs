using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess
{
    public class DbTableInfo
    {
        //列的信息
       public  List<DbColumnInfo> Columns { get; set; }
        //表名称
       public string TableName { get;  set; }

       public string[] PrimaryKey { get;  set; }

       
    }
}
