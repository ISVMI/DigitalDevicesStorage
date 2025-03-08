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
    public class GraphicalTabletsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public GraphicalTabletsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: GraphicalTablets
        public async Task<IActionResult> Index()
        {
            var digitalDevicesContext = _context.GraphicalTablets.Include(g => g.Manufacturer);
            return View(await digitalDevicesContext.ToListAsync());
        }

        // GET: GraphicalTablets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GraphicalTablets == null)
            {
                return NotFound();
            }

            var graphicalTablet = await _context.GraphicalTablets
                .Include(g => g.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graphicalTablet == null)
            {
                return NotFound();
            }

            return View(graphicalTablet);
        }

        // GET: GraphicalTablets/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: GraphicalTablets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Diagonal,Definition,FPS,MatrixType,ResponceTime,WorkWidth,WorkHeight,TabletDefinition,Sensivity,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] GraphicalTablet graphicalTablet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graphicalTablet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", graphicalTablet.ManufacturerId);
            return View(graphicalTablet);
        }

        // GET: GraphicalTablets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GraphicalTablets == null)
            {
                return NotFound();
            }

            var graphicalTablet = await _context.GraphicalTablets.FindAsync(id);
            if (graphicalTablet == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", graphicalTablet.ManufacturerId);
            return View(graphicalTablet);
        }

        // POST: GraphicalTablets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Diagonal,Definition,FPS,MatrixType,ResponceTime,WorkWidth,WorkHeight,TabletDefinition,Sensivity,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] GraphicalTablet graphicalTablet)
        {
            if (id != graphicalTablet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graphicalTablet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraphicalTabletExists(graphicalTablet.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", graphicalTablet.ManufacturerId);
            return View(graphicalTablet);
        }

        // GET: GraphicalTablets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GraphicalTablets == null)
            {
                return NotFound();
            }

            var graphicalTablet = await _context.GraphicalTablets
                .Include(g => g.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graphicalTablet == null)
            {
                return NotFound();
            }

            return View(graphicalTablet);
        }

        // POST: GraphicalTablets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GraphicalTablets == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.GraphicalTablets'  is null.");
            }
            var graphicalTablet = await _context.GraphicalTablets.FindAsync(id);
            if (graphicalTablet != null)
            {
                _context.GraphicalTablets.Remove(graphicalTablet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraphicalTabletExists(int id)
        {
          return (_context.GraphicalTablets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
