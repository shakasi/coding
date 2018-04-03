using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCompare.UIHelper;
using Shaka.Bll;
using Shaka.Model;
using Shaka.Utils;

namespace DBCompare
{
    public partial class Form1 : Form
    {

        private ListView[] _lvArray;
        private List<TableInfo> _tableListA;
        private List<TableInfo> _tableListB;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _lvArray = new ListView[] { listView1, listView2, listView3, listView4 };
            UICommonHelper.InitControl(_lvArray);

            listView1.Columns.Add("库A中多的表");
            listView2.Columns.Add("库B中多的表");
            listView3.Columns.Add("相同表");
            listView4.Columns.Add("差异表(点击见详细)");
            foreach (ListView lv in _lvArray)
            {
                lv.Columns[0].Width = lv.Width - 4;
            }
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            AnalyseTableBll analyseTableBll = new AnalyseTableBll();

            ConnInfo connInfoA = new ConnInfo();
            connInfoA.Ip = tbIPA.Text.Trim();
            connInfoA.Name = tbDBA.Text.Trim();
            connInfoA.UserId = tbUserNameA.Text.Trim();
            connInfoA.Pwd = tbPwdA.Text.Trim(); ;
            ConnInfo connInfoB = new ConnInfo();
            connInfoB.Ip = tbIPB.Text.Trim();
            connInfoB.Name = tbDBB.Text.Trim();
            connInfoB.UserId = tbUserNameB.Text.Trim();
            connInfoB.Pwd = tbPwdB.Text.Trim(); ;

            try
            {
                analyseTableBll.GetTableInfo(connInfoA, connInfoB, out _tableListA, out _tableListB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            foreach (ListView lv in _lvArray)
            {
                UICommonHelper.ClearListView(lv);
            }

            string compareFied = "TableName";
            // 求库A多于B
            var resultA = _tableListA.Except(_tableListB, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultA)
            {
                UICommonHelper.RefreshListView(listView1, item.TableName);
            }
            // 求库B多于A
            var resultB = _tableListB.Except(_tableListA, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultB)
            {
                UICommonHelper.RefreshListView(listView2, item.TableName);
            }

            #region 表名相同分析差异
            var sameTableName = _tableListA.Intersect(_tableListB, new ComparerHelper<TableInfo>(compareFied)).ToList();
            var diffTable = _tableListA.Except(_tableListB, new ComparerHelper<TableInfo>()).ToList();

            // 求AB相同的表
            var resultC = sameTableName.Except(diffTable, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultC)
            {
                UICommonHelper.RefreshListView(listView3, item.TableName);
            }

            // 求AB不同的表
            var resultD = sameTableName.Intersect(diffTable, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultD)
            {
                UICommonHelper.RefreshListView(listView4,item.TableName);
            }
            #endregion
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            ConnInfo connInfo = new ConnInfo();
            connInfo.Ip = tbIPA.Text.Trim();
            connInfo.Name = tbDBA.Text.Trim();
            connInfo.UserId = tbUserNameA.Text.Trim();
            connInfo.Pwd = tbPwdA.Text.Trim();

            TestConn(connInfo);
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            ConnInfo connInfo = new ConnInfo();
            connInfo.Ip = tbIPB.Text.Trim();
            connInfo.Name = tbDBB.Text.Trim();
            connInfo.UserId = tbUserNameB.Text.Trim();
            connInfo.Pwd = tbPwdB.Text.Trim(); ;

            TestConn(connInfo);
        }

        private void TestConn( ConnInfo connInfo )
        {
            try
            {
                ConnTestBll connTestBll = new ConnTestBll();
                connTestBll.ConnTest(connInfo);
                MessageBox.Show("连接成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败！" + ex.Message);
            }
        }

        private void listView4_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string tableName = (sender as ListView).SelectedItems[0].Text;
            List<TableInfo> tableInfoListA = new List<TableInfo>();
            List<TableInfo> tableInfoListB = new List<TableInfo>();
            foreach (TableInfo tb in _tableListA)
            {
                if (tb.TableName == tableName)
                {
                    tableInfoListA.Add(tb);
                }
            }
            foreach (TableInfo tb in _tableListB)
            {
                if (tb.TableName == tableName)
                {
                    tableInfoListB.Add(tb);
                }
            }

            FrmDiff frmDiff = new FrmDiff(tableInfoListA, tableInfoListB);
            frmDiff.ShowDialog();
        }
    }
}
