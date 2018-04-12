using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskAsyncHelper.RunAsync(AA, BB);
            Console.ReadLine();
        }

        public static void AA()
        {
            Thread.Sleep(3000);
            Console.WriteLine(string.Format("我是AA,{0}", DateTime.Now));
        }

        public static void BB()
        {
            Console.WriteLine(string.Format("我是BB,{0}", DateTime.Now));
        }
    }
}
