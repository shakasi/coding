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
    public partial class frmRevManage : Form
    {
        List<StoreResult> srList = new List<StoreResult>();
        public RMEnum RMEnumStatus { get; set; }
        public int SelectCount { get; set; }
        public string SelectStores { get; set; }

        //重试列表
        Dictionary<string, string> retryDic = new Dictionary<string, string>();

        public frmRevManage()
        {
            InitializeComponent();
        }

        private void frmRevManage_Load(object sender, EventArgs e)
        {
            lblNum.Text = SelectCount.ToString();
            BindListView();
            if (RMEnumStatus == RMEnum.待操作)
            {
                btnGiveup.Visible = false;
                btnRetry.Visible = false;
                chk_stores.Checked = true;
                chk_stores_Click(null, null);
                chk_stores.Enabled = false;
            }
        }

        private void BindDgvStores(RMEnum rmEnum,int status)
        {
            if (srList.Count > 0)
            {
                foreach (var item in srList)
                {
                    if (item.ResultCode == (int)RMEnum.操作成功 && status > 0)
                        item.IsCheck = true;

                    item.ResultCode = (int)rmEnum;
                    item.Result = rmEnum.ToString();
                   
                    if (item.StoreNumber == "021056" && status==0)
                    {
                        item.Result =RMEnum.操作成功.ToString();
                        item.ResultCode = (int)RMEnum.操作成功;
                        item.IsCheck = false;
                    }
                }
            }
            else
            {
                foreach (string s in SelectStores.Split(','))
                {
                    srList.Add(new StoreResult { IsCheck = true, StoreNumber = s, StoreName = s, ResultCode = (int)rmEnum, Result = rmEnum.ToString() });
                }
            }
            this.DgvStores.DataSource = srList;
            this.DgvStores.Refresh();
        }

        private void SetDgvStoresState()
        {
            for (int i = 0; i < DgvStores.Rows.Count; i++)
            {
                DataGridViewDisableCheckBoxCell checkCell = (DataGridViewDisableCheckBoxCell)DgvStores.Rows[i].Cells[0];
                string resultValue = DgvStores.Rows[i].Cells[2].Value.ToString();

                if (resultValue == RMEnum.操作失败.ToString())
                {
                    checkCell.Enabled = false;
                    checkCell.ReadOnly = true;
                    chk_stores.Enabled = true;
                }
                else
                {
                    checkCell.Enabled = true;
                    checkCell.ReadOnly = false;
                }
            }
            this.DgvStores.Refresh();
        }

        void BindListView()
        {
            this.DgvStores.AutoGenerateColumns = false;
            BindDgvStores(RMEnumStatus,0);

            this.DgvService.AutoGenerateColumns = false;
            this.DgvService.DataSource = REVServiceManage.ServiceList;
        }
        private void chk_stores_Click(object sender, EventArgs e)
        {
            if (chk_stores.Checked)
                SetAllRowChecked(true, DgvStores, "S_CHK");
            else
                SetAllRowChecked(false, DgvStores, "S_CHK");
        }

        private void SetAllRowChecked(bool chk, DataGridView dgv, string chkname)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewDisableCheckBoxCell checkCell = (DataGridViewDisableCheckBoxCell)dgv.Rows[i].Cells[chkname];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                string value = dgv.Rows[i].Cells[2].Value.ToString();
                if (flag != chk)
                    checkCell.Value = chk;

                if(value== RMEnum.操作成功.ToString())
                    checkCell.Value = false;

                if (value == RMEnum.操作处理中.ToString())
                    checkCell.Value = true;

                if (RMEnumStatus == RMEnum.待操作 && chk)
                {
                    checkCell.Enabled = true;
                    checkCell.ReadOnly = false;
                }
                continue;
            }
        }

        private void DgvStores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewDisableCheckBoxCell checkCell = (DataGridViewDisableCheckBoxCell)DgvStores.Rows[e.RowIndex].Cells[0];
                if (!checkCell.Enabled)
                {
                    string number = DgvStores.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if ((bool)DgvStores.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                        DgvStores.Rows[e.RowIndex].Cells[0].Value = false;
                    else
                        DgvStores.Rows[e.RowIndex].Cells[0].Value = true;

                    //反选
                    int i = 0;
                    foreach (DataGridViewRow row in DgvStores.Rows)
                    {
                        if ((bool)row.Cells[0].EditedFormattedValue || row.Cells[2].Value.ToString()== RMEnum.操作成功.ToString())
                            i++;
                    }
                    if (i == DgvStores.Rows.Count)
                        chk_stores.Checked = true;
                    else
                        chk_stores.Checked = false;
                }
            }
        }

        private void DgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (sender) as DataGridView;
            if (dgv.Columns[e.ColumnIndex].Name == "Open")
            {

                DataGridViewDisableButtonCell btnCell = (DataGridViewDisableButtonCell)dgv.Rows[e.RowIndex].Cells["Open"];
                string value = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (MessageBox.Show("确定开启"+ value + "吗?", "HQService 提示:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnCell.Value = "服务开启中";
                    btnCell.Enabled = false;
                    

                    bool result = false;
                    RMEnumStatus = RMEnum.操作处理中;
                    BindDgvStores(RMEnumStatus,0);
                    btnGiveup.Visible = true;
                    btnRetry.Visible = true;

                    if (result)
                    {
                        RMEnumStatus = RMEnum.操作成功;
                        BindDgvStores(RMEnumStatus,0);

                        MessageBox.Show("开启" + value + "成功！回到最初操作状态");

                        btnGiveup.Visible = false;
                        btnRetry.Visible = false;

                        RMEnumStatus = RMEnum.待操作;
                        BindDgvStores(RMEnumStatus,1);
                    }
                    else
                    {
                        RMEnumStatus = RMEnum.操作失败;
                        BindDgvStores(RMEnumStatus,0);
                        SetDgvStoresState();
                    }
                }       
            }

            if (dgv.Columns[e.ColumnIndex].Name == "Close")
            {
                string value = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (MessageBox.Show("确定关闭" + value + "吗?", "HQService 提示:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool result = true;
                    RMEnumStatus = RMEnum.操作处理中;
                    BindDgvStores(RMEnumStatus,0);
                    if (result)
                    {
                        RMEnumStatus = RMEnum.操作成功;
                    }
                    else
                    {
                        RMEnumStatus = RMEnum.操作失败;
                    }
                }
            }
        }

        private void btnIendl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnIenImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnCallBack_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnUpdImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnUPBizDate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnOtherUpload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnAutoUpdte_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点了" + (sender as Button).Text);
        }

        private void btnGiveup_Click(object sender, EventArgs e)
        {
            //对失败操作进行放弃处理
            if (MessageBox.Show("确定放弃对已选项的重试操作吗?", "HQService 提示:", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RMEnumStatus = RMEnum.待操作;
                BindDgvStores(RMEnumStatus,1);
                chk_stores.Enabled = false;
                chk_stores.Checked = true;
                SetDgvStoresState();
                
                this.btnGiveup.Visible = false;
                this.btnRetry.Visible = false;
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            //对失败操作进行重试
            if (MessageBox.Show("确定对已选项进行重试操作吗?", "HQService 提示:", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var r in retryDic)
                {
                     // 发送  r.Value  重试代码

                     //成功 or 失败
                }

                //重试成功后
                RMEnumStatus = RMEnum.待操作;
                chk_stores.Enabled = false;
                BindDgvStores(RMEnumStatus, 1);
                SetDgvStoresState();
                this.btnGiveup.Visible = false;
                this.btnRetry.Visible = false;
            }
        }
    }





    public class ColGroup
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class StoreResult
    {
        public bool IsCheck { get; set; }
        public string StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int ResultCode { get; set; }
        public string Result { get; set; }
    }

    public enum RMEnum { 待操作 = 1, 操作处理中 = 2, 操作成功 = 3, 操作失败 = 4 }

    public class REVServiceManage
    {
        //服务开关List
        private static List<ColGroup> serviceList;
        public static List<ColGroup> ServiceList
        {
            get
            {
                if (serviceList == null)
                {
                    serviceList = new List<ColGroup>();

                    serviceList.Add(new ColGroup { Code = 1, Name = "门店工具" });
                    serviceList.Add(new ColGroup { Code = 2, Name = "IIS服务" });
                    serviceList.Add(new ColGroup { Code = 3, Name = "数据库服务" });
                    serviceList.Add(new ColGroup { Code = 4, Name = "ConfigurationService" });
                    serviceList.Add(new ColGroup { Code = 5, Name = "FileDownLoadService" });
                    serviceList.Add(new ColGroup { Code = 6, Name = "ServiceMonitor" });
                    serviceList.Add(new ColGroup { Code = 7, Name = "PaymentService" });
                    serviceList.Add(new ColGroup { Code = 8, Name = "SyncDataToPOS" });
                    serviceList.Add(new ColGroup { Code = 9, Name = "FrontendService" });
                    serviceList.Add(new ColGroup { Code = 10, Name = "GatewayService" });
                }

                return serviceList;
            }
        }
    }
}
