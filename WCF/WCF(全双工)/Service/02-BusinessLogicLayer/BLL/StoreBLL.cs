using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.IDAL;

namespace Cuscapi.BLL
{
    public class StoreBLL
    {
        private IStore _storeDAL = Cuscapi.DALFactory.Factory.GetStoreDAL();

        public List<Cuscapi.Model.StoreInfo> GetStoreList(string group, string type, string value)
        {
            return _storeDAL.GetStoreList(group,type,value);
        }
    }
}
