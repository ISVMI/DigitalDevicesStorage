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
    public class MicrophonesController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public MicrophonesController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Microphones
        public async Task<IActionResult> Index()
        {
            var digitalDevicesContext = _context.Microphones.Include(m => m.Manufacturer);
            return View(await digitalDevicesContext.ToListAsync());
        }

        // GET: Microphones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Microphones == null)
            {
                return NotFound();
            }

            var microphone = await _context.Microphones
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (microphone == null)
            {
                return NotFound();
            }

            return View(microphone);
        }

        // GET: Microphones/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: Microphones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Principle,Direction,ExecutionType,AudioConnectionType,MinFrequency,MaxFrequency,Impedance,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Microphone microphone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(microphone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", microphone.ManufacturerId);
            return View(microphone);
        }

        // GET: Microphones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Microphones == null)
            {
                return NotFound();
            }

            var microphone = await _context.Microphones.FindAsync(id);
            if (microphone == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", microphone.ManufacturerId);
            return View(microphone);
        }

        // POST: Microphones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Principle,Direction,ExecutionType,AudioConnectionType,MinFrequency,MaxFrequency,Impedance,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] Microphone microphone)
        {
            if (id != microphone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(microphone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MicrophoneExists(microphone.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", microphone.ManufacturerId);
            return View(microphone);
        }

        // GET: Microphones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Microphones == null)
            {
                return NotFound();
            }

            var microphone = await _context.Microphones
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (microphone == null)
            {
                return NotFound();
            }

            return View(microphone);
        }

        // POST: Microphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Microphones == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Microphones'  is null.");
            }
            var microphone = await _context.Microphones.FindAsync(id);
            if (microphone != null)
            {
                _context.Microphones.Remove(microphone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MicrophoneExists(int id)
        {
          return (_context.Microphones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
