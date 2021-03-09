using Microsoft.AspNetCore.Mvc;
using RefArk.Customer.Models;
using RefArk.Station.Databases;
using System;

namespace RefArk.Customer.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StationController : Controller
    {

        [HttpGet]
        public async System.Threading.Tasks.Task<string> GetAsync()
        {
            StationDB stationDB = new StationDB();

            var station = await stationDB.GetCustomerByCarIDAsync("1");
            return station.CustomerID;
        }
    }
}
