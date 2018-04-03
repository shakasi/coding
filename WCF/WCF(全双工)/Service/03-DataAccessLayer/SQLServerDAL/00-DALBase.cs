using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.Utils;

namespace Cuscapi.SQLServerDAL
{
    public class DALBase
    {
        protected readonly SqlDBHelper _dbHelper;
        public DALBase()
        {
            _dbHelper = new SqlDBHelper();
        }
    }
}