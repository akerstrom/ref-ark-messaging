using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefArk.Customer.Models;

namespace RefArk.Customer.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public CustomerModel Get(int id)
        {
            CustomerModel customer = new CustomerModel() { Name = "Alice", Id = id };
            return customer;
        }
    }
}
