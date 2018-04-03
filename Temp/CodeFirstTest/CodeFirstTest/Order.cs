using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFirstTest
{
    class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public System.DateTime OrderDate { get; set; }
    
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
