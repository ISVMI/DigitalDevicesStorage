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
    public class WebCamsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public WebCamsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: WebCams

        public async Task<IActionResult> Index(string searchString=null, string criteria=null)
        {
            var digitalDevicesContext = _context.WebCams.Include(c => c.Manufacturer);
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

        // GET: WebCams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebCams == null)
            {
                return NotFound();
            }

            var webCam = await _context.WebCams
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webCam == null)
            {
                return NotFound();
            }

            return View(webCam);
        }

        // GET: WebCams/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            return View();
        }

        // POST: WebCams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MegaPixels,Definition,FPS,Microphone,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] WebCam webCam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webCam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", webCam.ManufacturerId);
            return View(webCam);
        }

        // GET: WebCams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebCams == null)
            {
                return NotFound();
            }

            var webCam = await _context.WebCams.FindAsync(id);
            if (webCam == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", webCam.ManufacturerId);
            return View(webCam);
        }

        // POST: WebCams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MegaPixels,Definition,FPS,Microphone,ConnectionType,Id,Price,Name,Model,Color,Warranty,ManufacturerId")] WebCam webCam)
        {
            if (id != webCam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webCam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebCamExists(webCam.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Id", webCam.ManufacturerId);
            return View(webCam);
        }

        // GET: WebCams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebCams == null)
            {
                return NotFound();
            }

            var webCam = await _context.WebCams
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webCam == null)
            {
                return NotFound();
            }

            return View(webCam);
        }

        // POST: WebCams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebCams == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.WebCams'  is null.");
            }
            var webCam = await _context.WebCams.FindAsync(id);
            if (webCam != null)
            {
                _context.WebCams.Remove(webCam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebCamExists(int id)
        {
          return (_context.WebCams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
