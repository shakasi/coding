using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Tablet.REV.Model;
//using Tablet.REV.Utils;

namespace Tablet.REV.SimpleAOP
{
    public class AopControlProxy : AopProxyBase
    {
        public AopControlProxy(MarshalByRefObject obj, Type type)
            : base(obj, type)   //指定调用基类中的构造函数
        {
        }

        public override void PreProcess(IMessage requestMsg)
        {
            //TODO

            
            return;
        }

        public override void PostProcess(IMessage requestMsg, IMessage Respond, object result,string useMessage)
        {
            //TODO.
            if (!(result is OperateResult))
            {
                return;
            }
            OperateResult operateResult = result as OperateResult;
            //LogHelper.WriteLog(operateResult.Message,useMessage);
            
            return;
        }
    }
}
