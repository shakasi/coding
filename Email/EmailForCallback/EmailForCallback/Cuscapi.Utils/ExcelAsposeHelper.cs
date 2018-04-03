using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Aspose.Cells;

namespace Cuscapi.Utils
{
  
    public class ExcelAsposeHelper
    {
        /// <summary> 
        /// 导出数据到本地 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="tableName">表格标题</param> 
        /// <param name="path">保存路径</param> 
        public static bool OutFileToDisk<T>(List<T> tList, string tableName, string path)
            where T:new()
        {
            if (tList==null && tList.Count<1)
            {
                return false;
            }

            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            PropertyInfo[] tPropertys = typeof(T).GetProperties();
            if (tList.Count > 0)
            {
                int Colnum = tPropertys.Length;//表格列数
                int Rownum = tList.Count;//表格行数

                //生成行1 标题行    
                cells.Merge(0, 0, 1, Colnum);//合并单元格 
                cells[0, 0].PutValue(tableName);//填写内容 
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);

                //生成行2 列名行 
                for (int i = 0; i < Colnum; i++)
                {
                    cells[1, i].PutValue(tPropertys[i].Name);
                    cells[1, i].SetStyle(style2);
                    cells.SetRowHeight(1, 25);
                }

                //生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int j = 0; j < Colnum; j++)
                    {
                        var proValue = tPropertys[j].GetValue(tList[i], null);
                        string value = proValue.ToString().Trim();
                        cells[2 + i, j].PutValue(value);
                        cells[2 + i, j].SetStyle(style3);
                    }
                    cells.SetRowHeight(2 + i, 24);
                }
            }
            workbook.Save(path);
            return true;
        }
    }
}
