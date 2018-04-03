using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess.DataBase
{
    public interface IDataBase
    {
        // 摘要: 
        //     返回实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        //
        // 返回结果: 
        //     System.Data.Common.DbCommand 的新实例。
         DbCommand CreateCommand();
        //
        // 摘要: 
        //     返回实现 System.Data.Common.DbConnection 类的提供程序的类的一个新实例。
        //
        // 返回结果: 
        //     System.Data.Common.DbConnection 的新实例。
        DbConnection CreateConnection();
        //
        // 摘要: 
        //     返回实现 System.Data.Common.DbDataAdapter 类的提供程序的类的一个新实例。
        //
        // 返回结果: 
        //     System.Data.Common.DbDataAdapter 的新实例。
        DbDataAdapter CreateDataAdapter();
        //
        // 摘要: 
        //     返回实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        //
        // 返回结果: 
        //     System.Data.Common.DbParameter 的新实例。
         DbParameter CreateParameter();
        //
        // 摘要: 
        //     返回提供程序的类的新实例，该实例可实现提供程序的 System.Security.CodeAccessPermission 类的版本。
        //
        // 参数: 
        //   state:
        //     System.Security.Permissions.PermissionState 值之一。
        //
        // 返回结果: 
        //     指定 System.Security.Permissions.PermissionState 的 System.Security.CodeAccessPermission
        //     对象。

        string DbParameterPrefix { get; }
    
    }
}
