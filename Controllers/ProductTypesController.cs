using DigitalDevices.DataContext;
using DigitalDevices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static DigitalDevices.Models.ProductTypeViewModel;

namespace DigitalDevices.Controllers
{
    [Authorize(Policy = "ProductsManagementPolicy")]
    public class ProductTypesController : Controller
    {
        private readonly DigitalDevicesContext _context;
        public ProductTypesController(DigitalDevicesContext context)
        {
            _context = context;
        }
        // GET: ProductTypesController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.ProductTypes != null ?
                          View(await _context.ProductTypes.ToListAsync()) :
                          Problem("Таблица 'Типы продуктов' пуста.");
        }

        // GET: ProductTypesController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }
            var productType = await _context.ProductTypes
                .Include(pt => pt.CharacteristicsTypeProductTypes)
                .ThenInclude(ctpt=>ctpt.CharacteristicsTypes)
                .FirstOrDefaultAsync(pt=>pt.Id == id);

            if (productType == null)
            {
                return NotFound();
            }
            var model = new ProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name,
                CharacteristicsTypes = productType.CharacteristicsTypeProductTypes.Select(ctpt => 
                new CharacteristicsTypesModel
                {
                    Name = ctpt.CharacteristicsTypes.Name,
                    DataType = ctpt.CharacteristicsTypes.DataType
                }).ToList()
            };

            return View(model);
        }

        // GET: ProductTypesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CharacteristicsTypes = new SelectList(_context.CharacteristicsType,"Name","Name");
            return View();
        }

        // POST: ProductTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CharacteristicsTypesNames")] CreateProductTypeViewModel productType)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(new ProductTypes
                {
                    Name = productType.Name
                });
                await _context.SaveChangesAsync();
                var newProductType = await _context.ProductTypes
                    .FirstOrDefaultAsync(pt=>pt.Name == productType.Name);
                if (newProductType != null) { 
                foreach (var characteristicType in productType.CharacteristicsTypesNames)
                {
                        var charType = await _context.CharacteristicsType
                            .FirstOrDefaultAsync(ct => ct.Name == characteristicType);
                        if(charType != null)
                        {
                             await _context.CharacteristicsTypeProductTypes.AddAsync(new CharacteristicsTypeProductTypes
                            {
                                ProductTypes = newProductType,
                                ProductTypesId = newProductType.Id,
                                CharacteristicsTypes = charType,
                                CharacteristicsTypeId = charType.Id
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypesController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            var model = new EditProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name
            };
            var existingCharacteristicsTypes = await _context.CharacteristicsTypeProductTypes
                .Where(ctptp => ctptp.ProductTypesId == productType.Id)
                .Select(ctpt=>ctpt.CharacteristicsTypes.Name).ToListAsync();
            var query = _context.CharacteristicsType
                .Select(ct=>ct);
            foreach (var existingCharacteristicsType in existingCharacteristicsTypes)
            {
                query = query.Where(ct => ct.Name != existingCharacteristicsType);
            }

            ViewBag.ExistingCharacteristicsTypes = existingCharacteristicsTypes;
            ViewBag.RestCharacteristicsTypes = await query.Select(ct=>ct.Name).ToListAsync();
            return View(model);
        }

        // POST: ProductTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CharacteristicTypesToAdd,CharacteristicTypesToDelete")] EditProductTypeViewModel productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProductType = await _context.ProductTypes
                    .FindAsync(productType.Id);

                if(existingProductType != null)
                {
                    existingProductType.Name = productType.Name;
                    if(productType.CharacteristicTypesToDelete.Any())
                    {
                        foreach (var charTypeName in productType.CharacteristicTypesToDelete)
                        {
                                var relationToRemove = await _context.CharacteristicsTypeProductTypes
                                    .FirstOrDefaultAsync(ctpt => ctpt.CharacteristicsTypes.Name == charTypeName
                                    && ctpt.ProductTypes.Id == existingProductType.Id);
                            if (relationToRemove != null)
                            {
                                _context.CharacteristicsTypeProductTypes.Remove(relationToRemove);
                            }
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (productType.CharacteristicTypesToAdd.Any())
                    {
                        foreach (var charTypeName in productType.CharacteristicTypesToAdd)
                        {
                            var charTypeToAdd = await _context.CharacteristicsType
                                .FirstOrDefaultAsync(ct => ct.Name == charTypeName);
                            if (charTypeToAdd != null)
                            {
                                await _context.CharacteristicsTypeProductTypes.AddAsync(new CharacteristicsTypeProductTypes
                                {
                                    ProductTypes = existingProductType,
                                    ProductTypesId = existingProductType.Id,
                                    CharacteristicsTypes = charTypeToAdd,
                                    CharacteristicsTypeId = charTypeToAdd.Id

                                });
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ExistingCharacteristicsTypes = await _context.CharacteristicsTypeProductTypes
        .Where(ctpt => ctpt.ProductTypesId == productType.Id)
        .Select(ctpt => ctpt.CharacteristicsTypes.Name)
        .ToListAsync();

            ViewBag.RestCharacteristicsTypes = await _context.CharacteristicsType
                .Where(ct => !CharacteristicTypeContainment(ct.Name))
                .Select(ct => ct.Name)
                .ToListAsync();
            return View(productType);
        }

        public bool CharacteristicTypeContainment(string name)
        {
            return ViewBag.ExistingCharacteristicsTypes.Contains(name);
        }
        // GET: ProductTypesController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(c => c.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.ProductTypes == null)
            {
                return Problem("Таблица 'Типы характеристик пуста!'");
            }
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
