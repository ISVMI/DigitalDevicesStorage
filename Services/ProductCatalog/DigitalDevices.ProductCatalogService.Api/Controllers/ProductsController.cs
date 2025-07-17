using DigitalDevices.ProductCatalogService.Api.Dtos;
using DigitalDevices.ProductCatalogService.Infrastructure.Data;
using DigitalDevices.ProductCatalogService.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DigitalDevices.ProductCatalogService.Api.Dtos.ProductsByTypeViewModel;

namespace DigitalDevices.ProductCatalogService.Api.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ProductCatalogContext _context;
        public ProductsController(ProductCatalogContext context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id,
            string productType,
            string currentFilter,
        string searchString,
        string sortField,
        string sortOrder,
        int? pageNumber,
        string filters)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductsByTypeViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Model = product.Model,
                Color = product.Color,
                Warranty = product.Warranty,
                Characteristics = product.CharacteristicsProduct
                    .Select(cp => new CharacteristicByType
                    {
                        CharacteristicType = cp.Key,
                        Value = cp.Value,
                    })
                    .ToList()
            };

            return Ok(model);
        }

        // GET: Products/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
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

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5 
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id,
            string productType,
            string currentFilter,
            string searchString,
            string sortField,
            string sortOrder,
            int? pageNumber,
            string filters)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
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
                ProductTypeId = product.ProductTypesId
            };

            return Ok(model);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(
        [FromForm] int Id,
        [FromForm] string Name,
        [FromForm] decimal Price,
        [FromForm] string Model,
        [FromForm] string Color,
        [FromForm] int Warranty,
        [FromForm] int ManufacturerId,
        [FromForm] int ProductTypeId)

        {
            var model = new EditProductViewModel
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Model = Model,
                Color = Color,
                Warranty = Warranty,
                ManufacturerId = ManufacturerId,
                ProductTypeId = ProductTypeId
            };
            var product = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null) 
            {
                return NotFound();
            }

            product.Id = model.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Model = model.Model;
            product.Color = model.Color;
            product.Warranty = model.Warranty;
            product.ManufacturerId = model.ManufacturerId;
            product.ProductTypesId = model.ProductTypeId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: Products/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string productType,
                        string currentFilter,
        string searchString,
        string sortField,
        string sortOrder,
        int? pageNumber,
        string filters,
        int quantity)
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
            return RedirectToAction(nameof(Index), new
            {
                productType,
                currentFilter,
                searchString,
                sortField,
                sortOrder,
                pageNumber,
                filters,
                quantity
            });
        }

        [HttpGet("ClearData")]
        public async Task<IActionResult> ClearData(string productType,
                        string currentFilter,
        string searchString,
        string sortField,
        string sortOrder,
        int? pageNumber,
        string filters)
        {
            if (await _context.Products.AnyAsync())
            {
                await _context.Products.ExecuteDeleteAsync();
            }
            return RedirectToAction(nameof(Index), new
            {
                productType,
                currentFilter,
                searchString,
                sortField,
                sortOrder,
                pageNumber,
                filters
            });
        }
    }
}
