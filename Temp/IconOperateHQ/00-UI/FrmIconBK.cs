using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Shaka.BLL;
using Shaka.Domain;
using Shaka.Infrastructure;

namespace Shaka.UI
{
    public partial class FrmIconBK : Form
    {
        private string _regexStr = string.Empty;
        private IconOperateBKBLL _bll = null;

        private string FoldPath
        {
            get
            {
                string fold = txtFold.Text.Trim();
                if (Directory.Exists(fold))
                {
                    return fold;
                }
                else
                {
                    return "";
                }
            }
            set { this.txtFold.Text = value; }
        }

        public FrmIconBK()
        {
            InitializeComponent();
        }

        private void FrmIconBK_Load(object sender, EventArgs e)
        {
            //测试
            FoldPath = ConfigurationManager.AppSettings["defaultPath"];

            _bll = new IconOperateBKBLL();
            _regexStr = ConfigurationManager.AppSettings["batchIconRegex"];

            ucPagination.Query += delegate (UC.UCPagination s)
            {
                Query(s);
            };

            ucPagination.PageSize = 10;
            Query(ucPagination);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FoldPath = dialog.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            if (FoldPath == "")
            {
                lblMsg.Text = "请先选择图片所在的文件夹！";
                txtFold.Focus();
                return;
            }

            List<FileInfo> fileList = new List<FileInfo>();
            if (!CommonHelper.GetFiles(FoldPath, _regexStr, ref fileList))
            {
                lblMsg.Text = "有文件不符合要求，请根据日志检查！";
                return;
            }

            ResultInfo result = _bll.SaveIcon(fileList);

            if (result == ResultInfo.Fail)
            {
                lblMsg.Text = "操作失败！";
            }
            else if (result == ResultInfo.Success)
            {
                lblMsg.Text = "操作成功！";
            }
            Query(ucPagination);
        }

        private void Query(UC.UCPagination s)
        {
            int totalRow = 0;
            List<IconBKInfo> iconList = _bll.QueryIcon(s.CurrentPage, s.PageSize,
                ref totalRow);
            dgvIcon.DataSource = iconList;
            s.TotalRecordCount = totalRow;
        }
    }
}
