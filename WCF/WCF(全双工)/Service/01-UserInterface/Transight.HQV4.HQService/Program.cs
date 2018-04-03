using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using log4net;

namespace Transight.HQV4.HQService
{
    static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string product = Application.ProductName;
                string appName = string.Format("{0}{1}{2}", Environment.CurrentDirectory, Path.DirectorySeparatorChar, product).Replace(Path.DirectorySeparatorChar, '_');
                bool isOnlyOne = false;
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, appName, out isOnlyOne);
                if (!isOnlyOne)
                {
                    MessageBox.Show(string.Format("{0} is running!", product));
                    Environment.Exit(1);
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //新加
                Application.ThreadException += Application_ThreadException;
                Application.Run(ConsoleFrm.GetInstance());
                //Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
                _log.Info("Error when invoke programme main entry throw exception." + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs ex)
        {
            _log.Info("Error when invoke [Application_ThreadException] method throw exception." + ex.Exception.Message + Environment.NewLine + ex.Exception.StackTrace);
        }
    }
}
