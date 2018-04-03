using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Tablet.REV.SimpleAOP
{
    /// <summary>
    /// AopProxyBase 抽象类 所有自定义AOP代理类都从此类派生，覆写IAopOperator接口，实现具体的前/后处理 。 
    /// </summary>
    public abstract class AopProxyBase : RealProxy, IAopOperator
    {
        private readonly MarshalByRefObject target; //默认透明代理 

        #region IAopOperator 成员（两个方法 一个在执行之前调用，另一个在执行之后调用，具体实现代码写在AopControlProxy类中）
        public abstract void PreProcess(IMessage requestMsg);  //方法执行的预处理逻辑
        public abstract void PostProcess(IMessage requestMsg, IMessage Respond,object result,string useMessage);  //方法执行结束的处理逻辑
        #endregion

        public AopProxyBase(MarshalByRefObject obj, Type type)
            : base(type)
        {
            this.target = obj;
        }

        #region Invoke 重写基方法
        public override IMessage Invoke(IMessage msg)
        {
            UseAopType useAspect = 0;
            string uselog = "";
            IMethodCallMessage call = (IMethodCallMessage)msg;

            //查询目标方法是否使用了启用AOP的MethodAopSwitcherAttribute
            foreach (Attribute attr in call.MethodBase.GetCustomAttributes(false))
            {
                MethodAopSwitcherAttribute mehodAopAttr = attr as MethodAopSwitcherAttribute;
                if (mehodAopAttr != null)
                {
                    useAspect = mehodAopAttr.UseAspect;
                    uselog = mehodAopAttr.Userlog;
                    break;
                    //if (mehodAopAttr.UseAspect==1)
                    //{
                    //    useAspect = 1;
                    //    break;
                    //} 
                }
            }

            if (useAspect == UseAopType.before)
            {
                this.PreProcess(msg);   //执行方法之前的操作
            }

            //如果触发的是构造函数，此时target的构建还未开始
            IConstructionCallMessage ctor = call as IConstructionCallMessage;
            if (ctor != null)
            {
                //获取最底层的默认真实代理
                RealProxy default_proxy = RemotingServices.GetRealProxy(this.target);

                default_proxy.InitializeServerObject(ctor);
                MarshalByRefObject tp = (MarshalByRefObject)this.GetTransparentProxy(); //自定义的透明代理 this

                return EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);
            }
            if (useAspect == UseAopType.before)
            {
                //#region 若不想运行目标方法可以执行该代码，如果直接return null会导致异常发生
                //IMethodCallMessage callMsg = msg as IMethodCallMessage;
                //return new ReturnMessage(callMsg.InArgs, null, 0, null, callMsg);
                //#endregion

                #region 调用目标方法代码
                IMethodMessage result_msg;
                result_msg = RemotingServices.ExecuteMessage(this.target, call);
                return result_msg;
                #endregion

            }
            else
            {
                #region 调用目标方法代码
                IMethodMessage result_msg;
                result_msg = RemotingServices.ExecuteMessage(this.target, call);
                object result = null;
                if (result_msg is ReturnMessage)
                {
                    result = (result_msg as ReturnMessage).ReturnValue;
                }
                //IMethodReturnMessage result_msg = RemotingServices.ExecuteMessage(this.target, call); 
                #endregion
                if (useAspect == UseAopType.after)
                {
                    this.PostProcess(msg, result_msg, result, uselog);      //执行方法结束后的操作
                }
                return result_msg;
            }
        }

        #endregion 
    }
}
