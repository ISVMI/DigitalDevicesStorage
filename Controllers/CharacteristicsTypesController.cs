using DigitalDevices.DataContext;
using DigitalDevices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DigitalDevices.Models.ProductTypeViewModel;

namespace DigitalDevices.Controllers
{
    public class CharacteristicsTypesController : Controller
    {
        private readonly DigitalDevicesContext _context;
        public CharacteristicsTypesController(DigitalDevicesContext context)
        {
            _context = context;
        }
        // GET: CharacteristicsTypesController
        public async Task<IActionResult> Index()
        {
            return _context.CharacteristicsType != null ?
                          View(await _context.CharacteristicsType.ToListAsync()) :
                          Problem("Entity set 'DigitalDevicesContext.Manufacturers'  is null.");
        }

        // GET: CharacteristicsTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharacteristicsTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DataType,EnumType")] CharacteristicsType characteristicsType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characteristicsType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characteristicsType);
        }

        // GET: CharacteristicsTypesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CharacteristicsType == null)
            {
                return NotFound();
            }

            var characteristicsType = await _context.CharacteristicsType.FindAsync(id);
            if (characteristicsType == null)
            {
                return NotFound();
            }
            return View(characteristicsType);
        }

        // POST: CharacteristicsTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DataType,EnumType")] CharacteristicsType characteristicsType)
        {
            if (id != characteristicsType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characteristicsType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacteristicsTypeExists(characteristicsType.Id))
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
            return View(characteristicsType);
        }

        // GET: CharacteristicsTypesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CharacteristicsType == null)
            {
                return NotFound();
            }

            var characteristicsType = await _context.CharacteristicsType
                .FirstOrDefaultAsync(c => c.Id == id);
            if (characteristicsType == null)
            {
                return NotFound();
            }

            return View(characteristicsType);
        }

        // POST: CharacteristicsTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.CharacteristicsType == null)
            {
                return Problem("Таблица Типы характеристик пуста!");
            }
            var characteristicsType = await _context.CharacteristicsType.FindAsync(id);
            if (characteristicsType != null)
            {
                _context.CharacteristicsType.Remove(characteristicsType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CharacteristicsTypeExists(int id)
        {
            return (_context.CharacteristicsType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
