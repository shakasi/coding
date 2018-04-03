using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCompare.UIHelper;
using Shaka.Utils;
using Shaka.Model;

namespace DBCompare
{
    public partial class FrmDiff : Form
    {
        private ListView[] _lvArray;
        private List<TableInfo> _tableInfoListA;
        private List<TableInfo> _tableInfoListB;

        public FrmDiff(List<TableInfo> tableInfoListA, List<TableInfo> tableInfoListB)
        {
            InitializeComponent();
            this._tableInfoListA = tableInfoListA;
            this._tableInfoListB = tableInfoListB;
        }

        private void FrmDiff_Load(object sender, EventArgs e)
        {

            _lvArray = new ListView[] { listView1, listView2, listView3 };
            UICommonHelper.InitControl(_lvArray);

            listView1.Columns.Add("左表多的列");
            listView2.Columns.Add("右表多的列");
            listView3.Columns.Add("相同列");
            foreach (ListView lv in _lvArray)
            {
                lv.Columns[0].Width = lv.Width - 4;
            }

            string compareFied = "ColumnName";
            // 左表多的列
            var resultA = _tableInfoListA.Except(_tableInfoListB, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultA)
            {
                UICommonHelper.RefreshListView(listView1,item.ColumnName);
            }
            // 右表多的列
            var resultB = _tableInfoListB.Except(_tableInfoListA, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultB)
            {
                UICommonHelper.RefreshListView(listView2, item.ColumnName);
            }

            #region 表名相同分析差异
            var sameColumnName = _tableInfoListA.Intersect(_tableInfoListB, new ComparerHelper<TableInfo>(compareFied)).ToList();
            var diffColumn = _tableInfoListB.Except(_tableInfoListA, new ComparerHelper<TableInfo>()).ToList();

            // 求相同的列
            var resultC = sameColumnName.Except(diffColumn, new ComparerHelper<TableInfo>(compareFied)).ToList();
            foreach (var item in resultC)
            {
                UICommonHelper.RefreshListView(listView3, item.ColumnName);
            }

            // 求不同列的具体信息
            StringBuilder diffMsgSB = new StringBuilder();
            foreach (TableInfo tbs in sameColumnName)
            {
                foreach (TableInfo tbd in diffColumn)
                {
                    //2表列名一样，其中有差异的列
                    if (tbs.ColumnName == tbd.ColumnName)
                    {
                        //比较，只找不同的列
                        string msgStr = CommonHelper.CompareEntity(tbs, tbd);
                        diffMsgSB.Append("\r\n\r\n" + tbs.ColumnName + ":\r\n  " + msgStr);
                        break;
                    }
                }
            }
            richTextBox1.Text = diffMsgSB.ToString().TrimStart("\r\n".ToArray());
            #endregion
        }
    }
}