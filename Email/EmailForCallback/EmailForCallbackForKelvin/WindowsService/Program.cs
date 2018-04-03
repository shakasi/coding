using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Reflection;
using log4net;

namespace WindowsService
{
    static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new EmailService()
            };

            log4net.Config.XmlConfigurator.Configure(new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"lib\log4net.config"));

            ServiceBase.Run(ServicesToRun);
        }
    }
}
