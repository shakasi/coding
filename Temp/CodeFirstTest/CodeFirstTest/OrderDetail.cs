using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFirstTest
{
    class OrderDetail
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string UnitPrice { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
