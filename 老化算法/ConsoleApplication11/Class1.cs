using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using Transight.Tablet.REV.HQ.UploadData;

namespace ConsoleApplication11
{
    public class Class1 : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Test()
        {
            //ErrorTryAgain(typeof(Class1), "Task", "小明");
            ErrorTryAgain(typeof(Class1), "TaskLog", "小明");
        }

        public static void Task(string a)
        {
            //throw new Exception("错了");
        }

        public static void TaskLog(string a)
        {
            _log.Info(string.Format("当前时间：{0}",DateTime.Now));
            throw new Exception("日志错了");
        }
    }
}
