using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shaka.Model;
using Shaka.Dal;

namespace Shaka.Bll
{
    public class ConnTestBll
    {
        public void ConnTest(ConnInfo connInfo)
        {
            ConnTestDal connTestDal = new ConnTestDal();
            string conn = string.Format("Data Source = {0};Initial Catalog = {1};User Id = {2};Password = {3};",
                connInfo.Ip, connInfo.Name, connInfo.UserId, connInfo.Pwd);
            connTestDal.ConnTest(conn);
        }
    }
}
