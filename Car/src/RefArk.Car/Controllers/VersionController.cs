using Microsoft.AspNetCore.Mvc;
using RefArk.Car.Models;

namespace RefArk.Car.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VersionController : Controller
    {

        [HttpGet]
        public VersionModel Get()
        {
            return new VersionModel() { Name = "RefArk.Car", Version = "1" };
        }
    }
}
