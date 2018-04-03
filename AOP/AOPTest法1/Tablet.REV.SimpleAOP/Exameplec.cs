using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablet.REV.SimpleAOP
{
    [AopProxyAttribute(typeof(AopControlProxyFactory))] //将自己委托给AOP代理AopControlProxy，（最好不要添加该代码）
    public class Exameplec : ContextBoundObject//放到特定的上下文中，该上下文外部才会得到该对象的透明代理
    {
        private string name;
        public Exameplec(string a)
        {
            this.name = a;
        }
        [MethodAopSwitcherAttribute(2, "参数")]
        public void say_hello()
        {
           // Console.WriteLine(name);
        }
        [MethodAopSwitcherAttribute(UseAopType.after, "参数")]
        public string sayByeBye()
        {
            return "abc";
            //Console.WriteLine(name);
        }
    }
}
