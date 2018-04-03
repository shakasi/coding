using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shaka.Dal;
using Shaka.Model;

namespace Shaka.Bll
{
    public class AnalyseTableBll
    {
        public void GetTableInfo(ConnInfo connInfoA, ConnInfo connInfoB, out List<TableInfo> tableListA, out List<TableInfo> tableListB)
        {
            AnalyseTableDal analyseTableDal = new AnalyseTableDal();
            string conn = "Data Source = {0};Initial Catalog = {1};User Id = {2};Password = {3};";
            string connA = string.Format(conn, connInfoA.Ip, connInfoA.Name, connInfoA.UserId, connInfoA.Pwd);
            string connB = string.Format(conn, connInfoB.Ip, connInfoB.Name, connInfoB.UserId, connInfoB.Pwd);
            analyseTableDal.GetTableInfo(connA, connB, out tableListA, out tableListB);
            HandleData(tableListA);
            HandleData(tableListB);
        }

        public void HandleData(List<TableInfo> tableList)
        {
            string tableName = string.Empty;
            foreach (TableInfo t in tableList)
            {
                if (!string.IsNullOrEmpty(t.TableName))
                {
                    tableName = t.TableName;
                }
                else
                {
                    t.TableName = tableName;
                }
            }
        }
    }
}
