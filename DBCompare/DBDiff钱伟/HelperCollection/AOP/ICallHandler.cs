using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{
    public interface ICallHandler
    {
        void BeginInvoke(MethodContext context);

        void EndInvoke(MethodContext context);

        void OnException(MethodContext context);
    }
}
