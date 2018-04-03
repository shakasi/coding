using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using log4net;

namespace Shaka.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string logPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Lib\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(logPath));

            Application.Run(new FrmIconBK());
        }
    }
}
