using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RefArk.Car.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Car.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SimulateDrivingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SimulateDrivingService _service;
        public SimulateDrivingController(IConfiguration configuration, SimulateDrivingService service) 
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public string StartDriving()
        {
            _service.StartDrivingAsync();
            return "Driving started";
        }
        [HttpDelete]
        public string StopDriving()
        {
            _service.StopAsync(System.Threading.CancellationToken.None);
            return "Driving stoped";
        }

    }
}
