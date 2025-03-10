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
    public class KeyboardsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public KeyboardsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Keyboards
        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.Keyboards.Include(c => c.Manufacturer);
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

        // GET: Keyboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Keyboards == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards
                .Include(k => k.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyboard == null)
            {
                return NotFound();
            }

            return View(keyboard);
        }

        // GET: Keyboards/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Keyboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Switches,Keycaps,LifeCycle,PushStrength,KeysCount,Material,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Keyboard keyboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", keyboard.ManufacturerId);
            return View(keyboard);
        }

        // GET: Keyboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Keyboards == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards.FindAsync(id);
            if (keyboard == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", keyboard.ManufacturerId);
            return View(keyboard);
        }

        // POST: Keyboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Type,Switches,Keycaps,LifeCycle,PushStrength,KeysCount,Material,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Keyboard keyboard)
        {
            if (id != keyboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keyboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyboardExists(keyboard.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", keyboard.ManufacturerId);
            return View(keyboard);
        }

        // GET: Keyboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Keyboards == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards
                .Include(k => k.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyboard == null)
            {
                return NotFound();
            }

            return View(keyboard);
        }

        // POST: Keyboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Keyboards == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Keyboards'  is null.");
            }
            var keyboard = await _context.Keyboards.FindAsync(id);
            if (keyboard != null)
            {
                _context.Keyboards.Remove(keyboard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyboardExists(int id)
        {
          return (_context.Keyboards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
