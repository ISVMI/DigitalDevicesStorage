using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices;
using DigitalDevices.Models;
using System.ComponentModel.DataAnnotations;
using DigitalDevices.Enums;
using Humanizer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using Bogus.DataSets;
using System.Drawing;

namespace DigitalDevices.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DigitalDevicesContext _context;

        public ProductsController(DigitalDevicesContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var digitalDevicesContext = _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductTypes);
            return View(await digitalDevicesContext.ToListAsync());
        }
        [HttpGet]
        public JsonResult GetDetails(int productTypeId)
        {

            var characteristics = _context.CharacteristicsType
                .Where(ct => ct.CharacteristicsTypeProductTypes.Any(ptc => ptc.ProductTypesId == productTypeId))
                .Select(ct => new
                {
                    ct.Id,
                    ct.Name,
                    Type = ct.DataType,
                    Values = ct.DataType == "enum"
                ? GetEnumValues(ct.EnumType)
                : null
                }).ToList();

            return Json(characteristics);
        }

        [HttpGet]
        public JsonResult GetCharacteristics(int productTypeId)
        {

            var characteristics = _context.CharacteristicsType
                .Where(ct => ct.CharacteristicsTypeProductTypes.Any(ptc => ptc.ProductTypesId == productTypeId))
                .Select(ct => new
                {
                    ct.Id,
                    ct.Name,
                    Type = ct.DataType,
                    Values = ct.DataType == "enum"
                ? GetEnumValues(ct.EnumType)
                : null
                }).ToList();

            return Json(characteristics);
        }
        private static Dictionary<string, string> GetEnumValues(string enumTypeName)
        {
            var enumType = Type.GetType($"DigitalDevices.Enums.{enumTypeName}");
            if (enumType == null || !enumType.IsEnum)
                return null;

            return Enum.GetValues(enumType)
                .Cast<object>()
                .ToDictionary(
                    e => e.ToString(),
                    e => ((Enum)e).Humanize()
                );
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ProductTypesId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productModel)
        {
            var product = new Product()
            {

                Price = productModel.Price,
                Name = productModel.Name,
                Model = productModel.Model,
                Color = productModel.Color,
                Warranty = productModel.Warranty,
                ManufacturerId = productModel.ManufacturerId,
                ProductTypesId = productModel.ProductTypesId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            foreach (var charInput in productModel.Characteristics)
            {
                var characteristic = new Characteristics()
                {
                    CharacteristicsTypeId = charInput.CharacteristicTypeId,
                    Value = charInput.Value
                };

                _context.Characteristics.Add(characteristic);
                await _context.SaveChangesAsync();
                _context.CharacteristicsProducts.Add(new CharacteristicsProduct()
                {
                    ProductId = product.Id,
                    CharacteristicsId = characteristic.Id
                });
            }
            await _context.SaveChangesAsync();

            return View(productModel);

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductViewModel newProduct = new()
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Model = product.Model,
                Color = product.Color,
                Warranty = product.Warranty,
                ManufacturerId = product.ManufacturerId,
                ProductTypesId = product.ProductTypesId
            };
            var cpConnection = _context.CharacteristicsProducts
            .Where(cp => cp.ProductId == product.Id).ToList();
            foreach (var cpt in cpConnection)
            {
                var characteristic = _context.Characteristics
                    .Where(c => c.Id == cpt.CharacteristicsId);
                newProduct.Characteristics.Add(characteristic.First());
            }
            var cptConnection = _context.CharacteristicsTypeProductTypes
                 .Where(cpt => cpt.ProductTypesId == product.ProductTypesId);
            foreach (var cp in cptConnection)
            {
                var characteristicsType = _context.CharacteristicsType
                    .Where(ct => ct.Id == cp.CharacteristicsTypeId);
                newProduct.CharacteristicsTypes.Add(characteristicsType.First());
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", newProduct.ManufacturerId);
            ViewData["ProductTypesId"] = new SelectList(_context.ProductTypes, "Id", "Name", newProduct.ProductTypesId);
            return View(newProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Model,Color,Warranty,ManufacturerId,ProductTypesId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", product.ManufacturerId);
            ViewData["ProductTypesId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypesId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DigitalDevicesContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
