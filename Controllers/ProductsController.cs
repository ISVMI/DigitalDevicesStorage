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
using static DigitalDevices.Models.EditProductViewModel;
using System.Reflection.PortableExecutable;

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

        public async Task<IActionResult> GetCharacteristicsToEdit(int productTypeId)
        {
            var characteristics = await _context.CharacteristicsTypeProductTypes
                .Where(ctpt => ctpt.ProductTypesId == productTypeId)
                .Select(ctpt => ctpt.CharacteristicsTypes)
                .ToListAsync();

            var viewModel = characteristics.Select(c => new ProductCharacteristicEditVM
            {
                CharacteristicTypeId = c.Id,
                Name = c.Name,
                DataType = c.DataType,
                EnumValues = c.DataType == "enum"
                    ? GetEnumValues(c.EnumType)
                    : null
            }).ToList();

            return PartialView("_CharacteristicsEditor", viewModel);
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
                var characteristic = new Models.Characteristics()
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
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Manufacturer)
        .Include(p => p.ProductTypes)
        .Include(p => p.CharacteristicsProduct)
            .ThenInclude(cp => cp.Characteristics)
                .ThenInclude(c => c.CharacteristicsType)
        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var model = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Model = product.Model,
                Color = product.Color,
                Warranty = product.Warranty,
                ManufacturerId = product.ManufacturerId,
                ProductTypeId = product.ProductTypesId,
                Characteristics = product.CharacteristicsProduct
                    .Select(cp => new ProductCharacteristicEditVM
                    {
                        CharacteristicTypeId = cp.Characteristics.CharacteristicsTypeId,
                        Name = cp.Characteristics.CharacteristicsType.Name,
                        Value = cp.Characteristics.Value,
                        DataType = cp.Characteristics.CharacteristicsType.DataType,
                        EnumValues = cp.Characteristics.CharacteristicsType.DataType == "enum"
                            ? GetEnumValues(cp.Characteristics.CharacteristicsType.EnumType)
                            : null
                    })
                    .ToList()
            };

            ViewBag.Manufacturers = await _context.Manufacturers.ToListAsync();
            ViewBag.ProductTypes = await _context.ProductTypes.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products
    .Include(p => p.CharacteristicsProduct)
        .ThenInclude(cp => cp.Characteristics)
    .FirstOrDefaultAsync(p => p.Id == model.Id);

                product.Name = model.Name;
                product.Price = model.Price;
                product.Model = model.Model;
                product.Color = model.Color;
                product.Warranty = model.Warranty;
                product.ManufacturerId = model.ManufacturerId;
                product.ProductTypesId = model.ProductTypeId;

                await UpdateProductCharacteristicsAsync(product, model.Characteristics);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private async Task UpdateProductCharacteristicsAsync(Product product, List<ProductCharacteristicEditVM> characteristics)
        {
            var existingCharacteristics = product.CharacteristicsProduct.ToList();
            _context.CharacteristicsProducts.RemoveRange(existingCharacteristics);

            foreach (var charVm in characteristics)
            {
                var characteristic = new Models.Characteristics
                {
                    CharacteristicsTypeId = charVm.CharacteristicTypeId,
                    Value = charVm.Value
                };

                product.CharacteristicsProduct.Add(new CharacteristicsProduct
                {
                    Products = product,
                    Characteristics = characteristic
                });
            }
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
