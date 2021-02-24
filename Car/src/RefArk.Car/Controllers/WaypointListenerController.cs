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
    public class WaypointListenerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly WaypointListenerService _service;
        public WaypointListenerController(IConfiguration configuration, WaypointListenerService service) 
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public string StartListening()
        {
            _service.StartListening();
            return "Listening started";
        }
        [HttpDelete]
        public string StopListening()
        {
            _service.StopListening();
            return "Listening stoped";
        }


    }
}
