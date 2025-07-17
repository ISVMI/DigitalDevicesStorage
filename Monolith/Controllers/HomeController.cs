using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Storage()
        {
            return View();
        }
    }
}
