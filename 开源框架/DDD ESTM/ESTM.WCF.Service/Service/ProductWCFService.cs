using ESTM.Common.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.WCF.Service
{
    [ServiceClass]
    public class ProductWCFService : IProductWCFService
    {
        public IList<DTO_TP_PRODUCT> GetAllProduct()
        {
            return new List<DTO_TP_PRODUCT>();
        }
    }
}
