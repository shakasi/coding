using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitySample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //属性注入
                //var container = new UnityContainer();
                //UnitySetup.Config(container);
                //Customer customer = new Customer();
                //container.BuildUp(customer);
                //customer.Save();

                //构造注入
                var container = new UnityContainer();
                UnitySetup.Config(container);
                //var sqlCustomer = container.Resolve<Customer>();
                var myqlCustomer = container.Resolve<Customer>();

                //sqlCustomer.Save();
                //Console.WriteLine();
                myqlCustomer.Save();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
    }

    public class UnitySetup
    {
        public static void Config(IUnityContainer container)
        {
            //属性注入
            //container.RegisterType<ICustomerDataAccess, CustomerSqlDataAccess>();
            //container.RegisterType<ICustomerDataAccess, CustomerSqlDataAccess>("mysql");

            //构造器注入
            container.RegisterType<ICustomerDataAccess, CustomerSqlDataAccess>();
            //container.RegisterType<ICustomerDataAccess, CustomerMysqlDataAccess>("mysql");
            //container.RegisterType<Customer>(
            //    new InjectionConstructor(new ResolvedParameter<ICustomerDataAccess>()));
            //container.RegisterType<Customer>("mysqlCustomer",
            //    new InjectionConstructor(new ResolvedParameter<ICustomerDataAccess>("mysql")));
        }
    }
}
