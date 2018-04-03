using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.AOP;

namespace Utility.HelperCollection.DataAccess.CallHandler
{
    public class EntityValueChangeCallHandler: ICallHandler
    {
        public EntityValueChangeCallHandler()
        { 
        
        }
        //private string _columnName;
        //public EntityValueChangeCallHandler(string columnName)
        //{
        //    _columnName = columnName;
        //}
        public void BeginInvoke(MethodContext context)
        {
           // throw new NotImplementedException();
        }
        public void EndInvoke(MethodContext context)
        {
            if (context.Exception != null) { return; }
            EntityBase entity = context.Executor as EntityBase;
            if (entity == null)
            {
                return;
            }
            if (context.MethodName.StartsWith("Set", StringComparison.CurrentCultureIgnoreCase))
            {
                string propertyName = context.MethodName.Substring(context.MethodName.IndexOf('_') + 1);
                PropertyInfo pi = entity.GetType().GetProperty(propertyName);
                object[] parameters = context.Parameters;
                if (pi != null && parameters.Length == 1) //如果存在该property
                {
                    if (parameters[0] != null && parameters[0].GetType() == pi.PropertyType)
                    {

                    }
                    else if (parameters[0] == null && !pi.PropertyType.IsValueType)
                    {

                    }
                    else
                    {
                        return;
                    }
                    entity.AddChangedField(propertyName, parameters[0]);
                }
            }
            //else if (context.MethodName.StartsWith("Get", StringComparison.CurrentCultureIgnoreCase))//获取熟悉的时候
            //{
            //    string propertyName = context.MethodName.Substring(context.MethodName.IndexOf('_') + 1);
            //    PropertyInfo pi = entity.GetType().GetProperty(propertyName);
            //    object[] parameters = context.Parameters;
            //    if (pi != null && parameters.Length == 0) //如果存在该property
            //    {
            //       //判断是否是DBNULL（如果是DBNULL则报错）要先用ISDBNULL方法判断是否是null 
            //    }
            //}
        }
        public void OnException(MethodContext context)
        {
            throw context.Exception;
        }
  
    }
}
