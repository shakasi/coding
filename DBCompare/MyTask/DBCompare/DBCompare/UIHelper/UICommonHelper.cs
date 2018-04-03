using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBCompare.UIHelper
{
    public class UICommonHelper
    {
        public static void RefreshListView(ListView lv,string value)
        {
            lv.Items.Add(value);
        }

        public static void InitControl(ListView[] lvArray)
        {
            foreach (ListView lv in lvArray)
            {
                ClearListView(lv);
                lv.GridLines = true; //显示表格线
                lv.View = View.Details;//显示表格细节
                lv.Scrollable = true;//有滚动条
                lv.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
                lv.FullRowSelect = true;//是否可以选择行
                //lv.CheckBoxes = true;
            }
        }

        public static void ClearListView(ListView lv)
        {
            //lv.Columns.Clear();
            lv.Items.Clear();
        }
    }
}
