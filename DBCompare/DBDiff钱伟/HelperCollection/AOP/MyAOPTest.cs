using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{
    public class MyAOPTest
    {
        [test]
      //  [tes1t("12333")]
        public virtual string say(string sss)
        {
            Console.WriteLine("the method say"+sss);
            //return sss;
           throw new Exception("123");
           // return null;
        }
        [test]
        
        public virtual int say(int i )
        {
            Console.WriteLine("the method say" + i.ToString());
            //return sss;
            //throw new Exception("123");
            return 0;
            // return null;
        }

        [tes1t]
        public virtual string pp
        {

            get;
            set;
        }
    }

    public class testAttribute : CallHandlerAttribute, ICallHandler
    {
        public override ICallHandler GetCallHandler()
        {
            return new testAttribute();
            //throw new NotImplementedException();
        }

        public void BeginInvoke(MethodContext context)
        {
           // throw new NotImplementedException();
            Console.WriteLine("1.before run "+context.MethodName);//+ context.MethodName);
            context.Processed = true;
           // throw new Exception("sdfsdfsad");
        }

        public void EndInvoke(MethodContext context)
        {
           // throw new NotImplementedException();
            Console.WriteLine("1.after run " + context.MethodName);// + context.MethodName);
        }

        public void OnException(MethodContext context)
        {
            //throw new NotImplementedException();
            Console.WriteLine("1.error at " + context.MethodName);// + context.MethodName+context.Exception.Message);
          //  throw context.Exception;
        }
    }
    public class tes1tAttribute : CallHandlerAttribute, ICallHandler
    {
        public tes1tAttribute()
        {

        }
        private string _ss;
        public tes1tAttribute(string ssssssss)
        {
            _ss = ssssssss;
        }
        public override ICallHandler GetCallHandler()
        {
            return this;
            //throw new NotImplementedException();
        }

        public void BeginInvoke(MethodContext context)
        {
            // throw new NotImplementedException();
            Console.WriteLine("2.before run " + context.MethodName+"     "+_ss);//+ context.MethodName);
        }

        public void EndInvoke(MethodContext context)
        {
            // throw new NotImplementedException();
            Console.WriteLine("2.after run " + context.MethodName);// + context.MethodName);
        }

        public void OnException(MethodContext context)
        {
            //throw new NotImplementedException();
            Console.WriteLine("2.error at " + context.MethodName);// + context.MethodName);
        }
    }


}
