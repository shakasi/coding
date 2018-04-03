using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new OrderContext("CodeFirstDb"))
            {

                var o = new Order();
                o.OrderDate = DateTime.Now;
                ctx.Orders.Add(o);
                ctx.SaveChanges();

                var query = from order in ctx.Orders
                            select order;
                foreach (var q in query)
                {
                    Console.WriteLine("OrderId:{0},OrderDate:{1}", q.Id, q.OrderDate);
                }

                Console.Read();
            }
        }
    }
}
