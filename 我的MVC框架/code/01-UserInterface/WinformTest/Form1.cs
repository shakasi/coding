using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Shaka.Utils;
//using log4net;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        //private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                LogDynamicHelper.CreateInstance().WriteLog(LogLevel.Exception, LogSeparator.CreateInstance("\r\n"), "JR1", new object[] { "1.0." + i, System.Reflection.MethodBase.GetCurrentMethod().Name, CommonHelper.GetLineNum(), "我是错误Exception" });
                LogDynamicHelper.CreateInstance().WriteLog(LogLevel.Error, LogSeparator.CreateInstance("\r\n"), "JR2", new object[] { "1.0." + i, System.Reflection.MethodBase.GetCurrentMethod().Name, CommonHelper.GetLineNum(), "我是错误Error" });
                LogDynamicHelper.CreateInstance().WriteLog(LogLevel.Warning, LogSeparator.CreateInstance("\r\n"), "JR3", new object[] { "1.0." + i, System.Reflection.MethodBase.GetCurrentMethod().Name, CommonHelper.GetLineNum(), "我是错误Warning" });
                LogDynamicHelper.CreateInstance().WriteLog(LogLevel.Information, LogSeparator.CreateInstance("\r\n"), "JR4", new object[] { "1.0." + i, System.Reflection.MethodBase.GetCurrentMethod().Name, CommonHelper.GetLineNum(), "我是错误Information" });
            }
        }
    }
}