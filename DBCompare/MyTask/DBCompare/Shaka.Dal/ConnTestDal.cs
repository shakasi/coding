using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shaka.Utils;

namespace Shaka.Dal
{
    public class ConnTestDal
    {
        public void ConnTest(string conn)
        {
            SqlDBHelper dbHelper = new SqlDBHelper(conn);
            dbHelper.OpenConn();
            dbHelper.CloseConn();
        }
    }
}
