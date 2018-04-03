using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Shaka.Infrastructure;
using Shaka.BLL;
using Shaka.Domain;

namespace Shaka.UI
{
    public partial class FrmIconOperate : Form
    {
        private int TextIconNumber
        {
            get 
            {
                string iconNumber = txtNumber.Text.Trim();
                if (iconNumber == "")
                {
                    return 0;
                }
                else
                {
                    int result;
                    if (int.TryParse(iconNumber, out result))
                    {
                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        private string TextIconName
        {
            get
            {
                return txtName.Text.Trim();
            }
        }

        public FrmIconOperate()
        {
            InitializeComponent();
        }

        private void frmIconOperate_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.RestoreDirectory = true;
            //file.InitialDirectory = "C:\\";
            file.Filter = "jpg,png文件|*.jpg;*.png|所有文件|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = file.FileName;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void dgvIcon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvIcon.SelectedRows)
            {
                txtNumber.Text = row.Cells["IconNumber"].Value.ToString();
                txtName.Text = row.Cells["IconName"].Value.ToString();
                break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TextIconNumber == -1)
            {
                MessageBox.Show("IconNumber输入不正确!");
                txtNumber.Focus();
                return;
            }
            string filePath = txtFile.Text.Trim();
            if (!File.Exists(filePath))
            {
                MessageBox.Show(string.Format("此文件不存在：{0}，请重新选择！", filePath));
                txtFile.Focus();
                return;
            }

            if (TextIconNumber==0)
            {
                if (MessageBox.Show("不输编号和编号不存在都会新增，确定这样？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    txtNumber.Focus();
                    return;
                }
            }

            if (TextIconName == "")
            {
                if (MessageBox.Show("不输名字会用图片文字+后缀名，确定这样？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    txtName.Focus();
                    return;
                }
            }
            
            IconOperateBLL bll = new IconOperateBLL();
            FileInfo file = new FileInfo(filePath);
            ResultInfo result = bll.SaveIcon(TextIconNumber, TextIconName, file);
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
            Query();
        }

        private void Query()
        {
            if (TextIconNumber == -1)
            {
                MessageBox.Show("IconNumber输入不正确!");
                txtNumber.Focus();
                return;
            }
            IconOperateBLL bll = new IconOperateBLL();
            List<IconInfo> iconList = bll.QueryIcon(TextIconNumber, TextIconName);
            dgvIcon.DataSource = iconList;
        }
    }
}