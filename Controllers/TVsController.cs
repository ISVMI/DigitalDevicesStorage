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
    public class TVsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public TVsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: TVs

        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.TVs.Include(c => c.Manufacturer);
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

        // GET: TVs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TVs == null)
            {
                return NotFound();
            }

            var tV = await _context.TVs
                .Include(t => t.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV == null)
            {
                return NotFound();
            }

            return View(tV);
        }

        // GET: TVs/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: TVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Diagonal,Definition,FPS,MatrixType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] TV tV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", tV.ManufacturerId);
            return View(tV);
        }

        // GET: TVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TVs == null)
            {
                return NotFound();
            }

            var tV = await _context.TVs.FindAsync(id);
            if (tV == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", tV.ManufacturerId);
            return View(tV);
        }

        // POST: TVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Diagonal,Definition,FPS,MatrixType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] TV tV)
        {
            if (id != tV.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TVExists(tV.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", tV.ManufacturerId);
            return View(tV);
        }

        // GET: TVs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TVs == null)
            {
                return NotFound();
            }

            var tV = await _context.TVs
                .Include(t => t.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV == null)
            {
                return NotFound();
            }

            return View(tV);
        }

        // POST: TVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TVs == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.TVs'  is null.");
            }
            var tV = await _context.TVs.FindAsync(id);
            if (tV != null)
            {
                _context.TVs.Remove(tV);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVExists(int id)
        {
          return (_context.TVs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
