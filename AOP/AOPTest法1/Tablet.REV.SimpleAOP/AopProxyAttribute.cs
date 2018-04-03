using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Tablet.REV.SimpleAOP
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AopProxyAttribute : ProxyAttribute
    {
        private IAopProxyFactory proxyFactory = null;

        public AopProxyAttribute(Type factoryType)
        {
            this.proxyFactory = (IAopProxyFactory)Activator.CreateInstance(factoryType);
        }

        #region 创建实例
        /// <summary>
        /// 获得目标对象的自定义透明代理
        /// </summary>
        public override MarshalByRefObject CreateInstance(Type serverType)//serverType是被AopProxyAttribute修饰的类
        {
            //未初始化的实例的默认透明代理
            MarshalByRefObject target = base.CreateInstance(serverType); //得到位初始化的实例（ctor未执行）
            object[] args = { target, serverType };
            //AopProxyBase rp = (AopProxyBase)Activator.CreateInstance(this.realProxyType ,args) ; //Activator.CreateInstance在调用ctor时通过了代理，所以此处将会失败

            //得到自定义的真实代理
            AopProxyBase rp = this.proxyFactory.CreateAopProxyInstance(target, serverType);//new AopControlProxy(target ,serverType) ;
            return (MarshalByRefObject)rp.GetTransparentProxy();
        }
        #endregion
    }
}
