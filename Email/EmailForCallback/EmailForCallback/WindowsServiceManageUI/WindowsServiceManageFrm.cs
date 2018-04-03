using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using log4net;
using Cuscapi.Utils;

namespace WindowsServiceManageUI
{
    public partial class WindowsServiceManageFrm : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public WindowsServiceManageFrm()
        {
            InitializeComponent();
            dtpDOTime.CustomFormat = "HH:mm";
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service\\Install";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;

            string msgStr = "安装成功,并启动";
            lblLog.Text = msgStr;
            _log.Info(msgStr);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service\\Install";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            lblLog.Text = "卸载成功";
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnPauseContinue_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController("ServiceShaka");
                if (serviceController.CanPauseAndContinue)
                {
                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        serviceController.Pause();
                        lblLog.Text = "服务已暂停";
                    }
                    else if (serviceController.Status == ServiceControllerStatus.Paused)
                    {
                        serviceController.Continue();
                        lblLog.Text = "服务已继续";
                    }
                    else
                    {
                        lblLog.Text = "服务未处于暂停和启动状态";
                    }
                }
                else
                {
                    lblLog.Text = "服务不能暂停";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSave(object sender, EventArgs e)
        {
            //保存时间
            string actionConfig = @"Service\ActionConfig.xml";
            try
            {
                XmlHelper xmlHelper = new XmlHelper(actionConfig);
                var doTime = dtpDOTime.Value.Hour + ":" + dtpDOTime.Value.Minute;
                xmlHelper.SetAttrValue("DoTime", "Value", doTime);
                xmlHelper.WriteXml();
            }
            catch (Exception ex)
            {
                _log.Fatal(string.Format("保存时间到配置文件：{0} 失败，将按原时间执行,详情：{1}", actionConfig,ex.Message));
            }
        }
    }
}
