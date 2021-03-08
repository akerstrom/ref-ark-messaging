using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RefArk.Customer.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Customer.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TripEndedEventController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TripEndedEventSubscriber _subscriber;
        public TripEndedEventController(IConfiguration configuration, TripEndedEventSubscriber subscriber)
        {
            _configuration = configuration;
            _subscriber = subscriber;
        }

        [HttpGet]
        public async Task<string> StartSubscribingAsync()
        {
            await _subscriber.StartSubscribing();
            return "Subscribing started";
        }
        [HttpDelete]
        public async Task<string> StopSubscribingAsync()
        {
            await _subscriber.StopSubscribing();
            return "Subscribing stoped";
        }
    }
}
