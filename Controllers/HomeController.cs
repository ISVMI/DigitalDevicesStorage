using DigitalDevices.DataContext;
using DigitalDevices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.Controllers
{
    public class HomeController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public HomeController(DigitalDevicesContext context)
        {
            _context = context;
        }
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
