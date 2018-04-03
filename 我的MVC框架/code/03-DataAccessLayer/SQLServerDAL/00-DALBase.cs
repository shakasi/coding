using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.SQLServerDAL
{
    public class DALBase
    {
        protected readonly Shaka.Utils.SqlDBHelper _dbHelper;
        public DALBase()
        {
            _dbHelper = new Utils.SqlDBHelper();
        }
    }
}