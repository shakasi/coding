using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication8
{
    class Class1
    {
        public void MTest()
        {
            int j = 30;
            int i = 0;
            while (i < j)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
                i++;
            }
        }
    }
}
