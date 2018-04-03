using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
    public abstract class CallHandlerAttribute:Attribute
    {
        public abstract ICallHandler GetCallHandler();
    }
}
