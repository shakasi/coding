using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Tablet.REV.SimpleAOP
{
    /// <summary>
    /// IAopOperator AOP操作符接口，包括前处理和后处理 
    /// </summary>
    public interface IAopOperator
    {
        void PreProcess(IMessage requestMsg);
        void PostProcess(IMessage requestMsg, IMessage Respond,object result,string useMessage);
    }
    /// <summary>
    /// IAopProxyFactory 用于创建特定的Aop代理的实例，IAopProxyFactory的作用是使AopProxyAttribute独立于具体的AOP代理类。
    /// </summary>
    public interface IAopProxyFactory
    {
        AopProxyBase CreateAopProxyInstance(MarshalByRefObject obj, Type type);
    }
}
