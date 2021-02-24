using Microsoft.AspNetCore.Mvc;
using RefArk.Customer.Models;

namespace RefArk.Customer.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VersionController : Controller
    {

        [HttpGet]
        public VersionModel Get()
        {
            return new VersionModel() { Name = "RefArk.Product", Version = "1" };
        }
    }
}
