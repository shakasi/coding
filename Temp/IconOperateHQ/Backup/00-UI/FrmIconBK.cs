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
using Shaka.Infrastructure;
using Shaka.BLL;
using Shaka.Domain;

namespace Shaka.UI
{
    public partial class FrmIconBK : Form
    {
        //要先保存，后上传
        private bool isSave = false;
        private string _regexStr=@"^\d+\.(jpg|gif|swf|jpeg)$";
        private int _sgNumber;
        private DateTime _effectiveDate;
        private HQIconOperateBLL _bll = null;

        private List<FileInfo> _fileList = null;
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

        public FrmIconBK(int sgNumber, DateTime effectiveDate)
        {
            InitializeComponent();
            this._sgNumber = sgNumber;
            this._fileList = new List<FileInfo>();
            this._effectiveDate = effectiveDate;
        }

        private void FrmIcon_Load(object sender, EventArgs e)
        {
            _bll = new HQIconOperateBLL();
            Query();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FoldPath = dialog.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FoldPath == "")
            {
                MessageBox.Show("请先选择图片所在的文件夹！");
                txtFold.Focus();
                return;
            }
            _fileList.Clear();
            CommonHelper.GetFiles(FoldPath, _regexStr, ref _fileList);

            List<HQSadDTICONInfo> sadDTIconList = new List<HQSadDTICONInfo>();
            foreach( FileInfo file in _fileList )
            {
                HQSadDTICONInfo sadDTIcon = new HQSadDTICONInfo();
                //只有组，没有门店，写死 “0”
                sadDTIcon.Storenum = "0";
                sadDTIcon.strgroup = _sgNumber;
                //sadDTIcon.Number
                sadDTIcon.IconNumber =Convert.ToInt32(file.Name.Substring(0,file.Name.LastIndexOf('.')));
                sadDTIcon.IconName = "Icon_" + sadDTIcon.IconNumber + "_" + file.Name;
                sadDTIcon.Status = true;
                sadDTIcon.effective_Date = DateTime.Now.Date;
                sadDTIcon.UpDT = DateTime.Now;
                sadDTIcon.Editor = "datascan";
                sadDTIcon.upd_seq = 0;
                sadDTIcon.Status = true;
                if (file.Extension == ".gif")
                {
                    sadDTIcon.IconType = 2;
                }
                else
                {
                    sadDTIcon.IconType = 1;
                }
            }

            ResultInfo result = _bll.SaveIcon(sadDTIconList);

            if (result == ResultInfo.Cover)
            {
                MessageBox.Show("覆盖成功！");
            }
            else if (result == ResultInfo.Increase)
            {
                MessageBox.Show("新增成功！");
            }
            else if (result == ResultInfo.Fail)
            {
                MessageBox.Show("操作失败！");
            }
            else if (result == ResultInfo.Success)
            {
                isSave = true;
                MessageBox.Show("操作成功！");
            }
            Query();
        }

        private void Query()
        {
            string whereStatement = "";
            
            int totalRow = 0;
            List<HQIconInfo> iconList = _bll.GetIcon("[Number], [Name], [Status]", "VW_RevIcon", whereStatement, "Number ASC",
                ucPagination.CurrentPage, ucPagination.PageRecordCount, "G", _sgNumber.ToString(), _effectiveDate.ToShortDateString(), ref totalRow);
            dgvIcon.DataSource = iconList;
            ucPagination.TotalRecordCount = totalRow;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (isSave)
            {
                MessageBox.Show("请先保存图片文件！");
                return;
            }
            else if (_fileList.Count==0)
            {
                MessageBox.Show("请选择图片目录！");
                txtFold.Focus();
                return;
            }
         
            string ftpServerIp = ConfigurationManager.AppSettings["ftpServerIp"];
            string path = ConfigurationManager.AppSettings["path"];
            string ftpUserId = ConfigurationManager.AppSettings["ftpUserId"];
            string ftpPassWord = ConfigurationManager.AppSettings["ftpPassWord"];

            FTPHelper ftpHelper = new FTPHelper(ftpServerIp, path, ftpUserId, ftpPassWord);

            try
            {
                foreach (FileInfo file in _fileList)
                {
                    //编辑图地址 ： ../Upload/Icon/Icon_1_夏威夷风光比萨.png?r=266512266
                    ftpHelper.Upload(file.FullName);
                }
                MessageBox.Show("上传成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("上传错误:{0}",ex.Message));
            }
        }
    }
}