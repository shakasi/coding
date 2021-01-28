using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitySample
{
    public interface ICustomerDataAccess
    {
        void Save(Customer c);
    }

    public class CustomerSqlDataAccess : ICustomerDataAccess
    {
        public void Save(Customer c)
        {
            Console.Write("{2}, save data id:{0},name{1}", c.Id, c.Name, this.GetType().ToString());
        }
    }

    public class CustomerMysqlDataAccess : ICustomerDataAccess
    {
        public void Save(Customer c)
        {
            Console.Write("{2}, save data id:{0},name{1}", c.Id, c.Name, this.GetType().ToString());
        }
    }

    public class Customer
    {
        [Dependency]
        public ICustomerDataAccess CustomerDataAccess { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        public Customer(ICustomerDataAccess customerDataAccess)
        {
            CustomerDataAccess = customerDataAccess;
        }

        public void Save()
        {
            CustomerDataAccess.Save(this);
        }
    }
}
