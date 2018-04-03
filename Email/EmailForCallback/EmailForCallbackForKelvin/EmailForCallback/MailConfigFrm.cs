using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using log4net;
using Cuscapi.Model;
using Cuscapi.BLL;

namespace EmailForCallback
{
    public partial class MailConfigFrm : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private EmailBLL _emailBLL = null;
        private EmailInfo _emailInfo = null;

        public MailConfigFrm()
        {
            InitializeComponent();
        }

        private void MailConfigFrm_Load(object sender, EventArgs e)
        {
            //添加俩个smpt服务器的名称
            cmbBoxSMTP.Items.Add(EmailProtocolInfo.Smtp163);
            cmbBoxSMTP.Items.Add(EmailProtocolInfo.SmtpGmail);
            cmbBoxSMTP.Items.Add(EmailProtocolInfo.SmtpQQ);
            //设置为下拉列表
            cmbBoxSMTP.DropDownStyle = ComboBoxStyle.DropDownList;
            //默认选中第一个选项
            cmbBoxSMTP.SelectedIndex = 0;

            _emailBLL = new EmailBLL();
            _emailInfo = _emailBLL.GetEntityFromXml();
            InitUI();
            _log.Info("邮件MailConfigFrm_Load");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //定义并初始化一个OpenFileDialog类的对象
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Application.StartupPath;
            openFile.FileName = "";
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;

            //显示打开文件对话框，并判断是否单击了确定按钮
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //得到选择的文件名
                string fileName = openFile.FileName;
                //将文件名添加到TreeView中
                treeViewFileList.Nodes.Add(fileName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断是否选中了节点
            if (treeViewFileList.SelectedNode != null)
            {
                //得到选择的节点
                TreeNode tempNode = treeViewFileList.SelectedNode;
                //删除选中的节点
                treeViewFileList.Nodes.Remove(tempNode);
            }
            else
            {
                MessageBox.Show("请选择要删除的附件。");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetEntityFromUI();
            _emailBLL.SaveEmailInfo(_emailInfo);

            //测试邮件发送
            SendEmail();
        }

        private void GetEntityFromXml()
        {
            _emailInfo = _emailBLL.GetEntityFromXml();
        }

        private void InitUI()
        {
            foreach (var item in cmbBoxSMTP.Items)
            {
                if (_emailInfo.SendServer.Equals(item.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    cmbBoxSMTP.SelectedItem = item;
                    break;
                }
            }
            txtDisplayName.Text = _emailInfo.DisplayName;
            txtUserName.Text = _emailInfo.UserName;
            txtPassword.Text = _emailInfo.Password;
            txtToName.Text = _emailInfo.ToName;
            txtEmail.Text = _emailInfo.ToEmail;
            txtSubject.Text = _emailInfo.ToSubject;
            rtxtBody.Text = _emailInfo.ToBody;
            foreach (string at in _emailInfo.AttachmentList)
            {
                treeViewFileList.Nodes.Add(at);
            }
        }

        private void GetEntityFromUI()
        {
            _emailInfo.SendServer = cmbBoxSMTP.SelectedItem.ToString();
            _emailInfo.DisplayName = txtDisplayName.Text;
            _emailInfo.UserName = txtUserName.Text;
            _emailInfo.Password = txtPassword.Text;
            _emailInfo.ToName = txtToName.Text;
            _emailInfo.ToEmail = txtEmail.Text;
            _emailInfo.ToSubject = txtSubject.Text;
            _emailInfo.ToBody = rtxtBody.Text;
            foreach (var at in treeViewFileList.Nodes)
            {
                _emailInfo.AttachmentList.Add(((System.Windows.Forms.TreeNode)at).Text);

            }
        }

        public void SendEmail()
        {
            _log.Info("SendEmail开始了");
            try
            {
                _emailBLL.SendEmail(_emailInfo);
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
            }
        }
    }
}