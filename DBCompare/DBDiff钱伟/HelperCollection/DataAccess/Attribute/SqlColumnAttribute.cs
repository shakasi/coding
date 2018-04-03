using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.AOP;
using Utility.HelperCollection.DataAccess.CallHandler;

namespace Utility.HelperCollection.DataAccess.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlColumnAttribute : CallHandlerAttribute
    {

        private string _columnName;
       // private bool _isGet;//是否需要选出来
        public SqlColumnAttribute(string columnName)
        {
            _columnName = columnName;
        }
        //public SqlColumnAttribute(string columnName,bool isGet)
        //{
        //    _columnName = columnName;
        //    _isGet = isGet;
        //}
        public string ColumnName { get { return _columnName; } }

        public override ICallHandler GetCallHandler()
        {
            return new EntityValueChangeCallHandler();
        }
    }
}
