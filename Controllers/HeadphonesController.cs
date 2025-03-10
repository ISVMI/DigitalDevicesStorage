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
    public class HeadphonesController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public HeadphonesController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Headphones
        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.Headphones.Include(c => c.Manufacturer);
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

        // GET: Headphones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Headphones == null)
            {
                return NotFound();
            }

            var headphones = await _context.Headphones
                .Include(h => h.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headphones == null)
            {
                return NotFound();
            }

            return View(headphones);
        }

        // GET: Headphones/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Headphones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeadphonesType,MaxFrequency,Sensivity,SoundSchemeFormat,Microphone,Connection,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Headphones headphones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(headphones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", headphones.ManufacturerId);
            return View(headphones);
        }

        // GET: Headphones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Headphones == null)
            {
                return NotFound();
            }

            var headphones = await _context.Headphones.FindAsync(id);
            if (headphones == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", headphones.ManufacturerId);
            return View(headphones);
        }

        // POST: Headphones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeadphonesType,MaxFrequency,Sensivity,SoundSchemeFormat,Microphone,Connection,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Headphones headphones)
        {
            if (id != headphones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headphones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeadphonesExists(headphones.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", headphones.ManufacturerId);
            return View(headphones);
        }

        // GET: Headphones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Headphones == null)
            {
                return NotFound();
            }

            var headphones = await _context.Headphones
                .Include(h => h.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headphones == null)
            {
                return NotFound();
            }

            return View(headphones);
        }

        // POST: Headphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Headphones == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Headphones'  is null.");
            }
            var headphones = await _context.Headphones.FindAsync(id);
            if (headphones != null)
            {
                _context.Headphones.Remove(headphones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeadphonesExists(int id)
        {
          return (_context.Headphones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
