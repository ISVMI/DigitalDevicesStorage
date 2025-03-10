using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices;
using DigitalDevices.Models;
using Humanizer;
using Microsoft.IdentityModel.Tokens;

namespace DigitalDevices.Controllers
{
    public class LaptopsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public LaptopsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Laptops
        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.Laptops.Include(c => c.Manufacturer);
            if (!searchString.IsNullOrEmpty())
            {
                switch (criteria)
                {
                    case "Операционная система":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.OS.Humanize().Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                    case "Модель процессора":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.CPModel.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
                    case "Видеокарта":
                        {
                            digitalDevicesContext = digitalDevicesContext
                        .Where(c => c.GPU.Contains(searchString))
                        .Include(c => c.Manufacturer);
                            break;
                        }
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

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .Include(l => l.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OS,CPModel,CPFrequency,CPcores,RAMType,RAMGB,GPU,GPUGB,StorageDriveType,StorageGB,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", laptop.ManufacturerId);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", laptop.ManufacturerId);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OS,CPModel,CPFrequency,CPcores,RAMType,RAMGB,GPU,GPUGB,StorageDriveType,StorageGB,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Laptop laptop)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", laptop.ManufacturerId);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Laptops == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .Include(l => l.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Laptops == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Laptops'  is null.");
            }
            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop != null)
            {
                _context.Laptops.Remove(laptop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
          return (_context.Laptops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
