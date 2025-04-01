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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Storage()
        {
            return View();
        }
    }
}
