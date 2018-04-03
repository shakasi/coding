using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Reflection;
using System.ServiceProcess;
using System.IO;
using log4net;
using Cuscapi.Utils;
using Cuscapi.BLL;

namespace WindowsService
{
    public partial class EmailService : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Timer _t = null;
        private int _intHour = 0;
        private int _intMinute = 0;
        private string _doDllStr = null;
        private string _doDllActionStr = null;
        private XmlHelper _xmlHelper = null;

        public EmailService()
        {
            InitializeComponent();

            string currentPath = GetType().Assembly.Location;
            currentPath = currentPath.Substring(0, currentPath.LastIndexOf(@"\")) + "\\";

            string actionConfig = currentPath + "ActionConfig.xml";
            if (!File.Exists(actionConfig))
            {
                _log.Fatal(string.Format("配置文件：{0} 不存在", actionConfig));
            }

            _xmlHelper = new XmlHelper(actionConfig);
            string hourMinute = _xmlHelper.GetAttrValue("DoTime", "Value");

            _doDllStr = _xmlHelper.GetAttrValue("DoAction", "Dll");
            _doDllActionStr = _xmlHelper.GetAttrValue("DoAction", "Action");

            try
            {
                _intHour = Convert.ToInt32(hourMinute.Split(':')[0]);
                _intMinute = Convert.ToInt32(hourMinute.Split(':')[1]);
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
            }
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;

            //为了测试方便，暂时注释掉了定时发送
            if (intHour == _intHour && intMinute == _intMinute && intSecond == 0) ///定时设置,判断分时,不看秒  
            {
                try
                {
                    System.Timers.Timer tt = (System.Timers.Timer)sender;
                    tt.Enabled = false;
                    DoAction();
                    tt.Enabled = true;
                }
                catch (Exception err)
                {
                    _log.Fatal(err.Message);
                }
            }
        }

        /// <summary>
        /// 这里应该读配置文件，用反射的方式执行服务挂载任务，这里先简写
        /// </summary>
        private void DoAction()
        {
            try
            {
                //以下不够灵活，暂时这样
                var objClass = FactoryHelper.GetInstance("Cuscapi.BLL", _doDllActionStr);
                if (objClass is EmailBLL)
                {
                    _log.Info("开始发送邮件");
                    (objClass as EmailBLL).SendEmail();
                    _log.Info("结束发送邮件");
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("发送邮件：" + ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("EmailService : 开始OnStart");

            _t = new Timer();
            _t.Interval = 1000;
            _t.Elapsed += T_Elapsed;
            _t.AutoReset = true;
            _t.Enabled = true;

            _log.Info("EmailService : 结束OnStart");
        }

        protected override void OnStop()
        {
            _t.Enabled = false;
            _log.Info("EmailService:OnStop");
        }

        protected override void OnContinue()
        {
            _t.Enabled = true;
            _log.Info("EmailService:OnContinue");
        }

        protected override void OnPause()
        {
            _t.Enabled = false;
            _log.Info("EmailService:OnPause");
        }
    }
}