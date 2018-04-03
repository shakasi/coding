using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using log4net;

namespace ConsoleApplication11
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(@"Ref\log4net.config"));
            
            try
            {
                Class1.Test();
                Console.Read();
            }
            catch (Exception ex)
            {

            }
            Console.Read();
        }
    }
}
