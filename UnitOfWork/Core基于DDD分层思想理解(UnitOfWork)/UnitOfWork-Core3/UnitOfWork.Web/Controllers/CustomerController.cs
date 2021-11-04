using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UnitOfWork.Customer;

namespace UnitOfWork.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerAppService customerAppService)
        {
            _logger = logger;
            _customerAppService = customerAppService;
        }

        [HttpGet]
        public ContentResult Get()
        {
            Customer.Customer customer = new Customer.Customer() { CustomerName = "shengjie" };
            try
            {
                _customerAppService.CreateCustomer(customer);
                _logger.LogInformation("执行成功！");
            }
            catch (Exception ex)
            {

            }
            return Content($"The shopping cart number of customer is {customer?.ShoppingCart?.Id}");
        }
    }
}
