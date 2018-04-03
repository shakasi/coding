using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOP_RealProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***\r\n Begin program - logging with dynamic proxy\r\n");
            // IRepository<Customer> customerRepository =
            //   new Repository<Customer>();
            // IRepository<Customer> customerRepository =
            //   new LoggerRepository<Customer>(new Repository<Customer>());
            IRepository<Customer> customerRepository =
              RepositoryFactory.Create<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\nEnd program - logging with dynamic proxy\r\n***");
            Console.ReadLine();
        }
    }
}