using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablet.REV.SimpleAOP
{
    public class AopControlProxyFactory : IAopProxyFactory
    {
        #region IAopProxyFactory 成员
        public AopProxyBase CreateAopProxyInstance(MarshalByRefObject obj, Type type)
        {
            return new AopControlProxy(obj, type);
        }
        #endregion
    }
}
