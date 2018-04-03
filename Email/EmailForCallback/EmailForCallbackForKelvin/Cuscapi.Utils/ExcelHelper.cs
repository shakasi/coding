using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace Cuscapi.Utils
{
    public class ExcelHelper
    {
        public static bool SaveDataToExcel<T>(List<T> tList, string tableName, string filePath)
        {
            Microsoft.Office.Interop.Excel.Application app =
                new Microsoft.Office.Interop.Excel.ApplicationClass();
            try
            {
                app.Visible = false;
                Workbook wBook = app.Workbooks.Add(true);
                Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
                PropertyInfo[] tPropertys = typeof(T).GetProperties();
                if (tList.Count>0)
                {
                    int row = 0;
                    row = tList.Count;
                    int col = tPropertys.Length;
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            var proValue = tPropertys[j].GetValue(tList[i], null);
                            if (proValue != DBNull.Value)
                            {
                                string str = Convert.ToString(proValue);
                                wSheet.Cells[i + 2, j + 1] = str;
                            }
                        }
                    }
                    for (int i = 0; i < col; i++)
                    {
                        wSheet.Cells[1, 1 + i] = tPropertys[i].Name;
                    }
                }

                //设置禁止弹出保存和覆盖的询问提示框
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                //保存工作簿
                wBook.Save();
                //保存excel文件
                app.Save(filePath);
                app.SaveWorkspace(filePath);
                app.Quit();
                app = null;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
