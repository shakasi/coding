using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tablet.REV.SimpleAOP;

namespace AOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Exameplec test = new Exameplec("小明");
            test.say_hello();
        }
    }
}