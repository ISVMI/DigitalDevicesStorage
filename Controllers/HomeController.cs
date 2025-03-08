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
            var model = new DataViewModel
            {
                ComputersData = _context.Computers.ToList(),
                GraphicalTabletsData = _context.GraphicalTablets.ToList(),
                HeadphonesData = _context.Headphones.ToList(),
                KeyboardsData = _context.Keyboards.ToList(),
                LaptopsData = _context.Laptops.ToList(),
                MiceData = _context.Mouse.ToList(),
                MicrophonesData = _context.Microphones.ToList(),
                MonitorsData = _context.Monitors.ToList(),
                TabletsData = _context.Tablets.ToList(),
                TVsData = _context.TVs.ToList(),
                WebCamsData = _context.WebCams.ToList(),
                Manufacturers = _context.Manufacturers.ToList()
            };
            
            return View(model);
        }
    }
}
