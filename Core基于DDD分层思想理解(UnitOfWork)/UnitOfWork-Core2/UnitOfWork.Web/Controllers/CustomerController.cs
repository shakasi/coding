using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Customer;

namespace UnitOfWork.Web.Controllers
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        public ContentResult Create()
        {
            Customer.Customer customer = new Customer.Customer() { CustomerName = "shengjie" };
            try
            {
                _customerAppService.CreateCustomer(customer);
            }
            catch (Exception ex)
            {

            }
            return Content($"The shopping cart number of customer is {customer?.ShoppingCart?.Id}");
        }
    }
}