using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices;
using DigitalDevices.Models;
using Microsoft.IdentityModel.Tokens;

namespace DigitalDevices.Controllers
{
    public class MonitorsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public MonitorsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Monitors
        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.Monitors.Include(c => c.Manufacturer);
            if (!searchString.IsNullOrEmpty())
            {
                switch (criteria)
                {
                    case "Наименование":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.Name.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                    case "Модель":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.Model.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                    case "Производитель":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.Manufacturer.Name.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                    default:
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.Name.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                }

            }
            return View(await digitalDevicesContext.ToListAsync());
        }

        // GET: Monitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Monitors == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitors
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        // GET: Monitors/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Monitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Diagonal,Definition,FPS,MatrixType,VideoConnector,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Models.Monitor monitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", monitor.ManufacturerId);
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Monitors == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", monitor.ManufacturerId);
            return View(monitor);
        }

        // POST: Monitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Diagonal,Definition,FPS,MatrixType,VideoConnector,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Models.Monitor monitor)
        {
            if (id != monitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitorExists(monitor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", monitor.ManufacturerId);
            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Monitors == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitors
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Monitors == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Monitors'  is null.");
            }
            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor != null)
            {
                _context.Monitors.Remove(monitor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitorExists(int id)
        {
          return (_context.Monitors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
