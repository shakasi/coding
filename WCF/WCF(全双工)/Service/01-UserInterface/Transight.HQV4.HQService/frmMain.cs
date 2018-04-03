using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Transight.HQV4.HQService
{
    public partial class frmMain : Form
    {
        Dictionary<string, string> diclist = new Dictionary<string, string>();
        public frmMain()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                pager1.PageSize = pager2.PageSize = 20;
                pager1.DrawControl(0);
                pager2.DrawControl(0);

                pager1.OnPageChanged += new EventHandler(Pager1_OnPageChanged);
                pager2.OnPageChanged += new EventHandler(Pager2_OnPageChanged);
            }
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            LoadData1();
        }

        private void Pager2_OnPageChanged(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void LoadData1()
        {
            int count;

            Cuscapi.BLL.StoreBLL aBLL = new Cuscapi.BLL.StoreBLL();
            this.dgvData.DataSource = aBLL.GetStoreList(this.cmbGroup.SelectedValue.ToString(),(this.cmbStore.SelectedIndex+1).ToString(),txtStore.Text);
        }

        private void LoadData2()
        {
            int count;

            Cuscapi.BLL.StoreBLL aBLL = new Cuscapi.BLL.StoreBLL();
            this.dgvFullData.DataSource = aBLL.GetStoreList(this.cmbGroup.SelectedValue.ToString(), (this.cmbStore.SelectedIndex + 1).ToString(), txtStore.Text);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            InitGroup();
            InitStore();

            InitVersion();
            InitHqApp();
            InitBkApp();
            InitData();

            BindDataStatus(cmbHqAppStatus);
            BindDataStatus(cmbBkAppStatus);
            BindDataStatus(cmbBkStatus);
            BindDataStatus(cmbOrderStatus);
            BindDataStatus(cmbDataStatus);
            BindDataStatus(cmbPlatStatus);



            //隐藏空白列
            //dgvData.RowHeadersVisible = false;

            //设置空白列的宽度不可改变
            //this.dgvData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //this.dgvFullData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            this.dgvData.AutoGenerateColumns = false;
            this.dgvFullData.AutoGenerateColumns = false;
        }

        #region //绑定数据
        private void InitData()
        {
            this.cmbData.Items.Clear();
            this.cmbData.Items.Add("门店工具");
            this.cmbData.Items.Add("Frontend service");
            this.cmbData.Items.Add("Monitor service");
            this.cmbData.Items.Add("TTG service");
            this.cmbData.Items.Add("TTC service");
            this.cmbData.Items.Add("刷卡器");
            this.cmbData.Items.Add("支付服务");
            this.cmbData.Items.Add("味千POS同步");
            this.cmbData.Items.Add("Transight POS同步");
            this.cmbData.Items.Add("文件下载");
            this.cmbData.SelectedIndex = 0;
        }

        private void InitBkApp()
        {
            this.cmbBkApp.Items.Clear();
            this.cmbBkApp.Items.Add("门店工具");
            this.cmbBkApp.Items.Add("Frontend service");
            this.cmbBkApp.Items.Add("Monitor service");
            this.cmbBkApp.Items.Add("TTG service");
            this.cmbBkApp.Items.Add("TTC service");
            this.cmbBkApp.Items.Add("刷卡器");
            this.cmbBkApp.Items.Add("支付服务");
            this.cmbBkApp.Items.Add("味千POS同步");
            this.cmbBkApp.Items.Add("Transight POS同步");
            this.cmbBkApp.Items.Add("文件下载");
            this.cmbBkApp.SelectedIndex = 0;
        }

        private void InitHqApp()
        {
            this.cmbHqApp.Items.Clear();
            this.cmbHqApp.Items.Add("FTP");
            this.cmbHqApp.Items.Add("IEN导出");
            this.cmbHqApp.Items.Add("HQ网站");
            this.cmbHqApp.Items.Add("HQ web service");
            this.cmbHqApp.Items.Add("紧急下发");
            this.cmbHqApp.Items.Add("报表服务");
            this.cmbHqApp.Items.Add("重定向服务");
            this.cmbHqApp.Items.Add("邮件服务");
            this.cmbHqApp.Items.Add("HQ上传");
            this.cmbHqApp.SelectedIndex = 0;
        }

        private void InitVersion()
        {
            this.cmbVersion.Items.Clear();
            this.cmbVersion.Items.Add("HQService");
            this.cmbVersion.Items.Add("HQTool");
            this.cmbVersion.Items.Add("HQWebService");
            this.cmbVersion.Items.Add("MailService");
            this.cmbVersion.Items.Add("RedirectService");
            this.cmbVersion.Items.Add("HQWebSite");
            this.cmbVersion.Items.Add("Backend");
            this.cmbVersion.Items.Add("REVTool");
            this.cmbVersion.Items.Add("TTC");
            this.cmbVersion.Items.Add("TTG");
            this.cmbVersion.Items.Add("FileDownloadService");
            this.cmbVersion.Items.Add("ServiceMonitor");
            this.cmbVersion.Items.Add("PaymentService");
            this.cmbVersion.Items.Add("SyncService(Ajisen)");
            this.cmbVersion.Items.Add("FrontendService");
            this.cmbVersion.SelectedIndex = 0;
        }

        private void BindDataStatus(ComboBox cmb)
        {
            cmb.Items.Clear();
            cmb.Items.Add("正常");
            cmb.Items.Add("异常");
            cmb.SelectedIndex = 0;
        }

        private void InitStore()
        {
            this.cmbStore.Items.Clear();
            this.cmbStore.Items.Add("门店编号");
            this.cmbStore.Items.Add("门店名称");
            this.cmbStore.SelectedIndex = 0;
        }

        private void InitGroup()
        {
            this.cmbGroup.Items.Clear();
            Cuscapi.BLL.GroupBLL aBLL = new Cuscapi.BLL.GroupBLL();
            var list = aBLL.AllGroup();
            //list.Insert(0, new Cuscapi.Model.GroupInfo() { GroupNumber = 0, GroupName = "请选择组别" });
            this.cmbGroup.DataSource = list;
            this.cmbGroup.DisplayMember = "GroupName";
            this.cmbGroup.ValueMember = "GroupNumber";
            this.cmbGroup.SelectedIndex = 0;
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.chkSelect.Checked = false;
            this.chkSelect2.Checked = false;

            diclist.Clear();

            LoadData1();
            LoadData2();
        }

        private void dgFullData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if ((bool)dgvFullData.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    dgvFullData.Rows[e.RowIndex].Cells[0].Value = false;
                else
                    dgvFullData.Rows[e.RowIndex].Cells[0].Value = true;

                //反选
                int i = 0;
                foreach (DataGridViewRow row in dgvFullData.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                        i++;
                }
                if (i == dgvFullData.Rows.Count)
                    chkSelect2.Checked = true;
                else
                    chkSelect2.Checked = false;

            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                string number = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
                if ((bool)dgvData.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                {
                    dgvData.Rows[e.RowIndex].Cells[0].Value = false;
                    if (diclist.ContainsKey(number)) diclist.Remove(number);

                }
                else
                {
                    dgvData.Rows[e.RowIndex].Cells[0].Value = true;
                    if (!diclist.ContainsKey(number)) diclist.Add(number, number);
                    
                }

                //反选
                int i = 0;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue) i++;
                }
                if (i == dgvData.Rows.Count) chkSelect.Checked = true; else chkSelect.Checked = false;

            }

            if (dgvData.Columns[e.ColumnIndex].Name == "search")
            {
                string value = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();

                #region //是否需要  选中改行
                ////选中改行
                //if ((bool)dgvData.Rows[e.RowIndex].Cells[0].EditedFormattedValue == false)
                //{
                //    dgvData.Rows[e.RowIndex].Cells[0].Value = true;
                //    if (!diclist.ContainsKey(value)) diclist.Add(value, value);
                //}
                #endregion

                tabControl1.SelectedIndex = 1;

                foreach (DataGridViewRow dgvr in dgvFullData.Rows)
                {
                    Cuscapi.Model.StoreInfo dr = (Cuscapi.Model.StoreInfo)dgvr.DataBoundItem;
                    if (dr.StoreNumber.Equals(value))
                    {
                        //去掉全选或以前选择项
                        chkSelect2.Checked = false; 
                        chkSelect2_Click(null,null);

                        //定位行并选中行
                        dgvr.Selected = true;
                        dgvFullData.CurrentCell = this.dgvFullData[0, dgvr.Index];
                        dgvr.Cells[0].Value = true;
                        return;
                    }
                }
            }
        }

        private void SetAllRowChecked(bool chk,DataGridView dgv,string chkname)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv.Rows[i].Cells[chkname];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag != chk) 
                    checkCell.Value = chk;

                if (dgv == dgvData)
                {
                    string number = dgvData.Rows[i].Cells[2].Value.ToString();
                    if (chk)
                    {
                        if (!diclist.ContainsKey(number)) diclist.Add(number, number);
                    }
                    else
                    {
                        if (diclist.ContainsKey(number)) diclist.Remove(number);
                    }
                            
                }
                continue;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                     
                    break;
                case 1:
                     
                    break;
            }
        }

        /// <summary>
        /// 门店管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOper_Click(object sender, EventArgs e)
        {
            string stores = string.Empty; 
            //获取选中数据
            foreach (var vv in diclist)
            {
                stores += vv.Key + ",";
            }

            if (string.IsNullOrWhiteSpace(stores))
            {
                MessageBox.Show("请在异常门店列表中选择要管理的门店！");
            }
            else
            {
                frmRevManage frm = new frmRevManage();
                frm.SelectCount = diclist.Count;
                frm.SelectStores = stores.TrimEnd(',');
                frm.RMEnumStatus = RMEnum.待操作;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dgvFullData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void chkSelect2_Click(object sender, EventArgs e)
        {
            if (chkSelect2.Checked)
                SetAllRowChecked(true, dgvFullData, "select2");
            else
                SetAllRowChecked(false, dgvFullData, "select2");

            
        }

        private void chkSelect_Click(object sender, EventArgs e)
        {
            if (chkSelect.Checked)
                SetAllRowChecked(true, dgvData, "select");
            else
                SetAllRowChecked(false, dgvData, "select");
        }
    }
}
