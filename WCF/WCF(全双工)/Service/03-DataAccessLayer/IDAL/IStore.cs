using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.IDAL
{
    public interface IStore
    {
        List<Cuscapi.Model.StoreInfo> GetStoreList(string group, string type, string value);
    }
}
