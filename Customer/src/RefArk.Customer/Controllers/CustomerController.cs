using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefArk.Customer.Models;
using RefArk.Customer.Databases;

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
        public CustomerModel Get(string id)
        {
            CustomerModel customer = new CustomerModel() { Name = "Alice", CustomerID = id };
            return customer;
        }

        [HttpGet("ByCar/{carID}")]
        public CustomerModel GetCustomerByCarID(string carID)
        {
            CustomerDB customerDB = new CustomerDB();
            CustomerModel customer = customerDB.GetCustomerByCarID(carID);
            return customer;
        }
    }
}
