using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices;
using DigitalDevices.Models;

namespace DigitalDevices.Controllers
{
    public class TabletsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public TabletsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Tablets
        public async Task<IActionResult> Index()
        {
            var digitalDevicesContext = _context.Tablets.Include(t => t.Manufacturer);
            return View(await digitalDevicesContext.ToListAsync());
        }

        // GET: Tablets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tablets == null)
            {
                return NotFound();
            }

            var tablet = await _context.Tablets
                .Include(t => t.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablet == null)
            {
                return NotFound();
            }

            return View(tablet);
        }

        // GET: Tablets/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Tablets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Diagonal,Definition,FPS,MatrixType,CPU,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Tablet tablet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", tablet.ManufacturerId);
            return View(tablet);
        }

        // GET: Tablets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tablets == null)
            {
                return NotFound();
            }

            var tablet = await _context.Tablets.FindAsync(id);
            if (tablet == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", tablet.ManufacturerId);
            return View(tablet);
        }

        // POST: Tablets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Diagonal,Definition,FPS,MatrixType,CPU,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Tablet tablet)
        {
            if (id != tablet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabletExists(tablet.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", tablet.ManufacturerId);
            return View(tablet);
        }

        // GET: Tablets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tablets == null)
            {
                return NotFound();
            }

            var tablet = await _context.Tablets
                .Include(t => t.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablet == null)
            {
                return NotFound();
            }

            return View(tablet);
        }

        // POST: Tablets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tablets == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Tablets'  is null.");
            }
            var tablet = await _context.Tablets.FindAsync(id);
            if (tablet != null)
            {
                _context.Tablets.Remove(tablet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabletExists(int id)
        {
          return (_context.Tablets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
