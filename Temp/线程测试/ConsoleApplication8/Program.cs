using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication8
{
    class Program
    {
        static Thread td = null;
        static void Main(string[] args)
        {
            
            AA();
            Thread.Sleep(5000);
            td.Abort();
            Console.Read();

        }

        static void AA()
        {
            try
            {
                td = new Thread(new Class1().MTest);
                //td.IsBackground = true;
                td.Start();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
