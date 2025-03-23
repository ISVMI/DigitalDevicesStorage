using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices.Models;
using Humanizer;
using static DigitalDevices.Models.EditProductViewModel;
using static DigitalDevices.Models.ProductTypeViewModel;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        public async Task<IActionResult> Index(
            string productType,
            string currentFilter,
            string searchString,
            string sortField,
            string sortOrder,
            int? pageNumber)
        {
            int pageSize = 10;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["PageNumber"] = pageNumber ??= 1;
            ViewData["ProductType"] = productType ?? "";
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var searchQuery = _context.Products
                 .Include(p => p.Manufacturer)
                 .Include(p => p.ProductTypes)
                 .Include(p => p.CharacteristicsProduct)
                 .Where(p => p.ProductTypes.Name.Contains(searchString)
                 || p.Manufacturer.Name.Contains(searchString)
                 || p.Name.Contains(searchString)
                 || p.Model.Contains(searchString));
                if (!searchQuery.Any())
                {
                    searchQuery = _context.Products
                     .Include(p => p.Manufacturer)
                     .Include(p => p.ProductTypes);
                }
                return View(await PaginatedList<Product>.CreateAsync(searchQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(productType))
            {
                ViewData["ProductType"] = productType;
                if (sortField.IsNullOrEmpty()
                    && sortOrder.IsNullOrEmpty())
                {
                    var productsOfType = _context.Products
                     .Include(p => p.Manufacturer)
                     .Include(p => p.ProductTypes)
                     .Where(p => p.ProductTypes.Name == productType);
                    if (productsOfType != null)
                    {
                        return View(await PaginatedList<Product>.CreateAsync(productsOfType.AsNoTracking(), pageNumber ?? 1, pageSize));
                    }
                }
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;

            ViewData["PriceSortOrder"] = sortField == "Price"
                ? (sortOrder == "asc" ? "desc" : "asc")
                : "asc";

            ViewData["WarrantySortOrder"] = sortField == "Warranty"
                ? (sortOrder == "asc" ? "desc" : "asc")
                : "asc";

            var query = _context.Products
                            .Include(p => p.Manufacturer)
                            .Include(p => p.ProductTypes)
                            .AsQueryable();

            if (!String.IsNullOrEmpty(productType))
            {
                query = _context.Products
                    .Where(p => p.ProductTypes.Name == productType)
                                .Include(p => p.Manufacturer)
                                .Include(p => p.ProductTypes)
                                .AsQueryable();
            }

            query = sortField switch
            {
                "Warranty" => sortOrder == "asc"
                    ? query.OrderBy(p => p.Warranty)
                    : query.OrderByDescending(p => p.Warranty),
                "Price" => sortOrder == "asc"
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Manufacturer)
            };
            return View(await PaginatedList<Product>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public JsonResult GetCharacteristics(int productTypeId)
        {

            var characteristics = _context.CharacteristicsType
                .Where(ct => ct.CharacteristicsTypeProductTypes
                .Any(ptc => ptc.ProductTypesId == productTypeId))
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
        public async Task<IActionResult> Details(int? id,
            string productType,
            string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(p => p.Manufacturer)
        .Include(p => p.ProductTypes)
        .Include(p => p.CharacteristicsProduct)
            .ThenInclude(cp => cp.Characteristics)
                .ThenInclude(c => c.CharacteristicsType)
        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductTypeViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Model = product.Model,
                Color = product.Color,
                Warranty = product.Warranty,
                Manufacturer = product.Manufacturer,
                ProductType = product.ProductTypes,
                Characteristics = product.CharacteristicsProduct
                    .Select(cp => new CharacteristicByType
                    {
                        CharacteristicType = cp.Characteristics.CharacteristicsType.Name,
                        Value = cp.Characteristics.Value,
                    })
                    .ToList()
            };
            ViewData["ProductType"] = productType;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            ViewBag.Manufacturers = await _context.Manufacturers.ToListAsync();
            ViewBag.ProductTypes = await _context.ProductTypes.ToListAsync();

            return View(model);
        }

        // GET: Products/Create
        public IActionResult Create(string productType,
            string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)
        {
            ViewData["ProductType"] = productType;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ProductTypesId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productModel,
            string productType,
            string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)
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
                var existingChars = await _context.Characteristics.Where(c => c.CharacteristicsTypeId == charInput.CharacteristicTypeId
                && c.Value == charInput.Value).ToListAsync();
                Characteristics existingChar = null;
                if (existingChars.Count == 0)
                {
                    existingChar = new Characteristics()
                    {
                        CharacteristicsTypeId = charInput.CharacteristicTypeId,
                        Value = charInput.Value ??= ""
                    };
                    _context.Characteristics.Add(existingChar);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    existingChar = existingChars.First();
                }


                _context.CharacteristicsProducts.Add(new CharacteristicsProduct()
                {
                    ProductId = product.Id,
                    CharacteristicsId = existingChar.Id
                });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index),
                new
                {
                    productType,
                    currentFilter,
                    searchString,
                    sortField,
                    sortOrder,
                    pageNumber
                });

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id,
            string productType,
            string currentFilter,
            string searchString,
            string sortField,
            string sortOrder,
            int? pageNumber)
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
                            : new()
                    })
                    .ToList()
            };
            ViewData["ProductType"] = productType;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString ??=currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            ViewBag.Manufacturers = await _context.Manufacturers.ToListAsync();
            ViewBag.ProductTypes = await _context.ProductTypes.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
    [FromForm] int Id,
    [FromForm] string Name,
    [FromForm] float Price,
    [FromForm] string Model,
    [FromForm] string Color,
    [FromForm] int Warranty,
    [FromForm] int ManufacturerId,
    [FromForm] int ProductTypeId,
    [FromForm] List<ProductCharacteristicEditVM> Characteristics,
    string productType,
string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)

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
                ProductTypeId = ProductTypeId,
                Characteristics = Characteristics
            };
            var product = await _context.Products
.Include(p => p.CharacteristicsProduct)
    .ThenInclude(cp => cp.Characteristics)
.FirstOrDefaultAsync(p => p.Id == Id);
            product.Id = model.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Model = model.Model;
            product.Color = model.Color;
            product.Warranty = model.Warranty;
            product.ManufacturerId = model.ManufacturerId;
            product.ProductTypesId = model.ProductTypeId;

            await UpdateProductCharacteristicsAsync(product, model.Characteristics);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    productType,
                    currentFilter,
                    searchString,
                    sortField,
                    sortOrder,
                    pageNumber
                });

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
                    Value = charVm.Value ??= ""
                };

                product.CharacteristicsProduct.Add(new CharacteristicsProduct
                {
                    Products = product,
                    Characteristics = characteristic
                });
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id,
            string productType,
            string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)
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
            ViewData["ProductType"] = productType;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string productType,
                        string currentFilter,
string searchString,
string sortField,
string sortOrder,
int? pageNumber)
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
                pageNumber
            });
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
