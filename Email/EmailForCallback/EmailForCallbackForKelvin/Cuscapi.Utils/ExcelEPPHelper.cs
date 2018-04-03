using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.IO;
using OfficeOpenXml;
//使用EPP时，请勿修改EPP源码

namespace Cuscapi.Utils
{
    public class ExcelEPPHelper
    {
        public static bool SaveDataToExcel<T>(List<T> tList, string tableName, string filePath,List<string> colNameList)
        {
            try
            {
                FileInfo newFile = new FileInfo(filePath);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(filePath);
                }
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(tableName);

                    PropertyInfo[] tPropertys = typeof(T).GetProperties();
                    if (tList.Count > 0)
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
                                    worksheet.Cells[i + 2, j + 1].Value = str;
                                }
                            }
                        }
                        for (int i = 0; i < col; i++)
                        {
                            // worksheet.Cells[1, 1 + i].Value = tPropertys[i].Name;
                            worksheet.Cells[1, 1 + i].Value = colNameList[i];
                        }
                    }
                    package.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
