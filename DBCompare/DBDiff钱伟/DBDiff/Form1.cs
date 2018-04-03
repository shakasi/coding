using DBDiff.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.HelperCollection.DataAccess;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace DBDiff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string strCon1;
        string strCon2;
        private void btnAnalyse_Click(object sender, EventArgs e)
        {
           // StopWatcher
             strCon1 = string.Format("User ID={0};Data Source={1};Password={2}", txtUserName1.Text, txtSid1.Text, txtPassword1.Text);
             strCon2 = string.Format("User ID={0};Data Source={1};Password={2}", txtUserName2.Text, txtSid2.Text, txtPassword2.Text);
            EntityCollection<OracleTableList> tableList1 = GetTableListByName(txtDBUser.Text.ToUpper(), strCon1);
            EntityCollection<OracleTableList> tableList2 = GetTableListByName(txtDBUser.Text.ToUpper(), strCon2);

            EntityCollection<OracleTableColumnList> tableColumn1 = GetTableColumnListByName(txtDBUser.Text, strCon1);
            EntityCollection<OracleTableColumnList> tableColumn2 = GetTableColumnListByName(txtDBUser.Text, strCon2);
            AnalyseTableDiff(tableList1, tableList2);
            if (bothIn2Table.Count > 0)
            {
                AnalyseTableColumnDiff(bothIn2Table, tableColumn1.ToList(), tableColumn2.ToList()); 
            }
            listView1.Columns.Clear();
            this.listView1.GridLines = true; //显示表格线
            this.listView1.View = View.Details;//显示表格细节
            this.listView1.Scrollable = true;//有滚动条
            this.listView1.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
            this.listView1.FullRowSelect = true;//是否可以选择行
            this.listView1.Columns.Add("表名");
            this.listView1.Columns.Add("差异");
            listView1.CheckBoxes = true;
            //this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView2.Columns.Clear();
            this.listView2.GridLines = true; //显示表格线
            this.listView2.View = View.Details;//显示表格细节
            this.listView2.Scrollable = true;//有滚动条
            this.listView2.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
            this.listView2.FullRowSelect = true;//是否可以选择行
            this.listView2.Columns.Add("列名");
            this.listView2.Columns.Add("差异");

            //this.listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            foreach (TableDiffInfo diff in tableDiff)
            { 
                
                ListViewItem item=new ListViewItem ();
                item.SubItems[0].Text=diff.TableName;
                item.Tag = diff;
                item.SubItems.Add(diff.DiffReason);
               // List<TableColumnDiffInfo> columnDiff = tableColumnDiff.FindAll(new Predicate<TableColumnDiffInfo>((obj) => { return obj.TableName == diff.TableName; }));
                listView1.Items.Add(item);
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.ItemSelectionChanged += (object sender1, ListViewItemSelectionChangedEventArgs e1) =>
            {
                listView2.Items.Clear();
               string tableName =  e1.Item.Text;
               TableDiffInfo diff = tableDiff.Find(new Predicate<TableDiffInfo>((obj) => { return obj.TableName == tableName; }));
               List<TableColumnDiffInfo> columnDiff = tableColumnDiff.FindAll(new Predicate<TableColumnDiffInfo>((obj) => { return obj.TableName == tableName; }));
               foreach (TableColumnDiffInfo subdiff in columnDiff)
               {
                   ListViewItem item = new ListViewItem();
                   item.SubItems[0].Text = subdiff.ColumName;
                   item.SubItems.Add(subdiff.DiffReason);
                   listView2.Items.Add(item);
               }
               this.listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
               richTextBox1.Clear();
               if (diff.UpdateSql != null)
               {
                   foreach (string s in diff.UpdateSql)
                   {
                       richTextBox1.AppendText(s);
                   }
               }
            };

        }


        private EntityCollection<OracleTableList> GetTableListByName(string name, string strCon1)
        {
            DBHelperBase<OracleTableList> helper = new DBHelperBase<OracleTableList>(DataBaseType.Oracle9i, strCon1);
            OracleTableList searchCondition = helper.CreateSimpleSeachCondition();
            searchCondition.Owner = name;
            EntityCollection<OracleTableList> tableList1 = helper.SelectDataByCondition(searchCondition, false);//用条件查询
            return tableList1;
        }
        private EntityCollection<OracleTableColumnList> GetTableColumnListByName(string name, string strConn)
        {
            DBHelperBase<OracleTableColumnList> helper = new DBHelperBase<OracleTableColumnList>(DataBaseType.Oracle9i, strConn);
            OracleTableColumnList searchCondition = helper.CreateSimpleSeachCondition();
            searchCondition.Owner = name;
            EntityCollection<OracleTableColumnList> tableColumnList = helper.SelectDataByCondition(searchCondition, false);
            return tableColumnList;
        }

        List<OracleTableList> inTable1NotInTable2;
        List<OracleTableList> bothIn2Table;
        List<OracleTableList> inTable2NotInTable1;
        List<TableDiffInfo> tableDiff;
        List<TableColumnDiffInfo> tableColumnDiff;
        private void AnalyseTableDiff( EntityCollection<OracleTableList> table1,EntityCollection<OracleTableList> table2)
        {
          tableDiff = new List<TableDiffInfo>();
           inTable1NotInTable2= table1.Except(table2, new OracleTableListComparer()).ToList();
           bothIn2Table = table1.Intersect(table2, new OracleTableListComparer()).ToList();
           inTable2NotInTable1 = table2.Except(table1, new OracleTableListComparer()).ToList();
           foreach (OracleTableList table in inTable1NotInTable2)
           {
               TableDiffInfo diff = new TableDiffInfo();
               diff.TableName = table.Owner + "." + table.TableName;
               //diff.DiffReason = "表在A库中存在但在B库中不存在";
               diff.DiffReason = "老库存在新库中没有的表";
               if (diff.UpdateSql == null)
               {
                   diff.UpdateSql = new List<string>();
               }
               diff.UpdateSql.Add( GetDDLForTable(table.TableName, table.Owner));
               tableDiff.Add(diff);
           }

           //foreach (OracleTableList table in inTable2NotInTable1)
           //{
           //    TableDiffInfo diff = new TableDiffInfo();
           //    diff.TableName = table.Owner + "." + table.TableName;
           //    diff.DiffReason = "表在B库中存在但在A库中不存在";
           //    tableDiff.Add(diff);
           //}
        }
        private string GetDDLForTable(string tableName,string owner)
        {
            using (System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(strCon1))
            {
                System.Data.OracleClient.OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("select SYS.DBMS_METADATA.GET_DDL('TABLE','{0}','{1}') from dual", tableName, owner);
                conn.Open();
                object result =  cmd.ExecuteScalar();
                return result.ToString();
                
            }
        }
        private void AnalyseTableColumnDiff(List<OracleTableList> tables, List<OracleTableColumnList> relatedColumns1, List<OracleTableColumnList> relatedColumns2)
        {
            tableColumnDiff = new List<TableColumnDiffInfo>();
            foreach (OracleTableList table in tables)
            {
               List<OracleTableColumnList> columns1 = relatedColumns1.FindAll(new Predicate<OracleTableColumnList>((obj) => { return obj.Owner == table.Owner && obj.TableName == table.TableName; }));
               List<OracleTableColumnList> columns2 = relatedColumns2.FindAll(new Predicate<OracleTableColumnList>((obj) => { return obj.Owner == table.Owner && obj.TableName == table.TableName; }));
               List<OracleTableColumnList> columns = columns1.Union(columns2, new OracleTableColumnListComparer()).ToList();
               bool hasDiff = false;
               bool isCross = false; //含新库库中没有的列
               bool hasNewField = false;//不含新库库中的列
               bool hasAttrDiff = false; //列属性存在不同
               List<string> sqls = new List<string>();
               string[] dataBaseTypeWithoutLength = new string[] { "BLOB", "CLOB", "DATE", "LONG" };
               foreach (OracleTableColumnList col in columns)
               {
                   //修改由于A做基础库 
                   //bool 
                   OracleTableColumnList colInTable1 = columns1.Find(new Predicate<OracleTableColumnList>((obj) => { return obj.Owner == col.Owner && obj.TableName == col.TableName && obj.ColumnName == col.ColumnName; }));
                   OracleTableColumnList colInTable2 = columns2.Find(new Predicate<OracleTableColumnList>((obj) => { return obj.Owner == col.Owner && obj.TableName == col.TableName && obj.ColumnName == col.ColumnName; }));
                   if (colInTable1 == null && colInTable2 == null)
                   {
                       continue;
                   }
                   else if (colInTable1 != null && colInTable2 == null)
                   {
                       TableColumnDiffInfo diff = new TableColumnDiffInfo();
                       diff.TableName = col.Owner + "." + col.TableName;
                       diff.ColumName = col.ColumnName;
                       diff.DiffReason = "列在A库的表中但不再B库的表中";
                       tableColumnDiff.Add(diff);
                       hasNewField = true;
                       hasDiff = true;
                       string s = "";
                       if (colInTable1.DataType == "NUMBER")
                       {
                           if (colInTable1.DataScale!=null&&colInTable1.DataPrecision!=null)
                           {
                               s = string.Format("alter table {4}.{0} add {1} {2}({5},{6}) {3};", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner, colInTable1.DataPrecision, colInTable1.DataScale);
                           }
                           else if (colInTable1.DataScale == null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} add {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataPrecision + ")", colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                           else
                           {
                               s = string.Format("alter table {4}.{0} add {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                       }
                       else
                       {
                           s = string.Format("alter table {4}.{0} add {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataLength + ")", colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                       }
                       sqls.Add(s);
                   }
                   else if (colInTable1 == null && colInTable2 != null)
                   {
                       TableColumnDiffInfo diff = new TableColumnDiffInfo();
                       diff.TableName = col.Owner + "." + col.TableName;
                       diff.ColumName = col.ColumnName;
                       diff.DiffReason = "列在B库的表中但不再A库的表中";
                       tableColumnDiff.Add(diff);
                       isCross = true;
                       hasDiff = true;
                   }
                   else if (colInTable1.Nullable != colInTable2.Nullable)
                   {
                       TableColumnDiffInfo diff = new TableColumnDiffInfo();
                       diff.TableName = col.Owner + "." + col.TableName;
                       diff.ColumName = col.ColumnName;
                       diff.DiffReason = "列的是否可空不同";
                       tableColumnDiff.Add(diff);
                       hasAttrDiff = true;
                       hasDiff = true;
                       string s = "";
                       if (colInTable1.DataType == "NUMBER")
                       {
                           if (colInTable1.DataScale != null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2}({5},{6}) {3};", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner, colInTable1.DataPrecision, colInTable1.DataScale);
                           }
                           else if (colInTable1.DataScale == null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataPrecision + ")", colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                           else
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                       }
                       else
                       {
                           s = string.Format("alter table {4}.{0} modify {1} {2} {3};", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataLength + ")", colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                       }
                       sqls.Add(s);
                   }
                   else if (colInTable1.DataType != colInTable2.DataType)
                   {
                       TableColumnDiffInfo diff = new TableColumnDiffInfo();
                       diff.TableName = col.Owner + "." + col.TableName;
                       diff.ColumName = col.ColumnName;
                       diff.DiffReason = "列的类型不同";
                       tableColumnDiff.Add(diff);
                       hasAttrDiff=true;
                       hasDiff = true;
                       string s = "";
                       if (colInTable1.DataType == "NUMBER")
                       {
                           if (colInTable1.DataScale != null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2}({5},{6}) ;", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner, colInTable1.DataPrecision, colInTable1.DataScale);
                           }
                           else if (colInTable1.DataScale == null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataPrecision + ")", colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                           else
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : " null ", colInTable1.Owner);
                           }
                       }
                       else
                       {
                           s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataLength + ")", "", colInTable1.Owner);
                       }
                       sqls.Add(s);

                   }
                   else if (colInTable1.DataLength != colInTable2.DataLength)
                   {
                       TableColumnDiffInfo diff = new TableColumnDiffInfo();
                       diff.TableName = col.Owner + "." + col.TableName;
                       diff.ColumName = col.ColumnName;
                       diff.DiffReason = "列的数据长度不同";
                       tableColumnDiff.Add(diff);
                       hasAttrDiff=true;
                       hasDiff = true;
                       string s = "";
                       if (colInTable1.DataType == "NUMBER")
                       {
                           if (colInTable1.DataScale != null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2}({5},{6}) ;", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : "", colInTable1.Owner, colInTable1.DataPrecision, colInTable1.DataScale);
                           }
                           else if (colInTable1.DataScale == null && colInTable1.DataPrecision != null)
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataPrecision + ")", colInTable1.Nullable == "N" ? " not null " : "", colInTable1.Owner);
                           }
                           else
                           {
                               s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, colInTable1.DataType, colInTable1.Nullable == "N" ? " not null " : "", colInTable1.Owner);
                           }
                       }
                       else
                       {
                           s = string.Format("alter table {4}.{0} modify {1} {2} ;", colInTable1.TableName, colInTable1.ColumnName, dataBaseTypeWithoutLength.Contains(colInTable1.DataType) ? "DATE" : colInTable1.DataType + "(" + colInTable1.DataLength + ")",  "", colInTable1.Owner);
                       }
                       sqls.Add(s);
                   }


               }
               if (hasDiff)
               {
                   TableDiffInfo diff = new TableDiffInfo();
                   diff.TableName = table.Owner + "." + table.TableName;
                   diff.DiffReason ="";
                   diff.UpdateSql = sqls;
                   if (hasAttrDiff)
                   {
                      diff.DiffReason =diff.DiffReason +"|存在列属性不同";
                   }
                   if (hasNewField)
                   {
                      diff.DiffReason =diff.DiffReason +"|缺少列";
                   }
                   if (isCross)
                   {
                       diff.UpdateSql = null;
                       diff.DiffReason = "|老库存在新库中没有的字段";
                   }
                  
                   tableDiff.Add(diff);
               
               }
            }
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = true;
            }
        }

        private void btnSelReverse_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked =false;
            }
        }

        private void btnExportAllSQL_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.ShowDialog();
            if (!string.IsNullOrEmpty(file.FileName))
            {
                using (FileStream fs = (FileStream)file.OpenFile())
                {
                    foreach (ListViewItem item in listView1.CheckedItems)
                    {

                        TableDiffInfo diff = item.Tag as TableDiffInfo;
                        if (diff == null || diff.UpdateSql == null || diff.UpdateSql.Count == 0)
                        {
                            continue;
                        }
                       // byte[] bts1 = System.Text.Encoding.UTF8.GetBytes(System.Environment.NewLine);
                        byte[] bts = System.Text.Encoding.UTF8.GetBytes(string.Join(System.Environment.NewLine, diff.UpdateSql));
                        byte[] bts1 = System.Text.Encoding.UTF8.GetBytes(System.Environment.NewLine + "/" + System.Environment.NewLine);
                        fs.Write(bts, 0, bts.Length);
                        fs.Write(bts1, 0, bts1.Length);
                    }
                }
            }
        }

    }
    public class OracleTableListComparer : IEqualityComparer<OracleTableList>
    {
        public bool Equals(OracleTableList x, OracleTableList y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.TableName == y.TableName && x.Owner==y.Owner;
        }

        public int GetHashCode(OracleTableList obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashTableName = obj.TableName == null ? 0 : obj.TableName.GetHashCode();

            int hashTableOwner = obj.Owner == null ? 0 : obj.Owner.GetHashCode();

            return hashTableName ^ hashTableOwner;
        }
    }


    public class OracleTableColumnListComparer : IEqualityComparer<OracleTableColumnList>
    {
        public bool Equals(OracleTableColumnList x, OracleTableColumnList y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.TableName == y.TableName && x.Owner == y.Owner&&x.ColumnName==y.ColumnName;
        }

        public int GetHashCode(OracleTableColumnList obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashProductName = obj.TableName == null ? 0 : obj.TableName.GetHashCode();

            int hashProductCode = obj.Owner == null ? 0 : obj.Owner.GetHashCode();

            int hashProductCode1 = obj.Owner == null ? 0 : obj.ColumnName.GetHashCode();

            return hashProductName ^ hashProductCode ^ hashProductCode1;
        }
    }
}
