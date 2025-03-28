using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalDevices.Models;
using Humanizer;
using static DigitalDevices.Models.EditProductViewModel;
using static DigitalDevices.Models.ProductTypeViewModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using NuGet.Versioning;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;

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
        int? pageNumber,
        string filters)
        {
            NumberFormatInfo provider = new()
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = "."
            };
            pageNumber ??= 1;
            int pageSize = 10;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["PageNumber"] = pageNumber ??= 1;
            ViewData["ProductType"] = productType ?? "";
            var query = _context.Products
    .Include(p => p.Manufacturer)
    .Include(p => p.ProductTypes)
    .Include(p => p.CharacteristicsProduct)
        .ThenInclude(cp => cp.Characteristics)
    .AsQueryable();

            if (!String.IsNullOrEmpty(productType))
            {
                query = query.Where(p => p.ProductTypes.Name == productType);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.ProductTypes.Name.Contains(searchString)
                    || p.Manufacturer.Name.Contains(searchString)
                    || p.Name.Contains(searchString)
                    || p.Model.Contains(searchString));

                if (!query.Any())
                {
                    query = _context.Products
                     .Include(p => p.Manufacturer)
                     .Include(p => p.ProductTypes);
                }
                if (!String.IsNullOrEmpty(productType))
                {
                    query = query.Where(p => p.ProductTypes.Name == productType);
                }
                if (String.IsNullOrEmpty(sortField)
                    && String.IsNullOrEmpty(sortOrder))
                {
                    return View(await PaginatedList<Product>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize));
                }
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(productType))
            {
                ViewData["ProductType"] = productType;
                if (sortField.IsNullOrEmpty()
                    && sortOrder.IsNullOrEmpty()
                    && filters.IsNullOrEmpty())
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
            ViewData["FilterList"] = filters;

            ViewData["PriceSortOrder"] = sortField == "Price"
                ? (sortOrder == "asc" ? "desc" : "asc")
                : "asc";

            ViewData["WarrantySortOrder"] = sortField == "Warranty"
                ? (sortOrder == "asc" ? "desc" : "asc")
                : "asc";

            if (!String.IsNullOrEmpty(filters))
            {
                var filterObj = JsonConvert.DeserializeObject<FilterModel>(filters);

                filterObj ??= new FilterModel();

                if (filterObj.Price.Min != null
                    || filterObj.Price.Max != null)
                {
                    if (filterObj.Price.Min.HasValue && filterObj.Price.Max.HasValue)
                    {
                        query = query
                            .Where(p => p.Price >= filterObj.Price.Min
                            && p.Price <= filterObj.Price.Max);
                    }
                    else
                    {
                        if (filterObj.Price.Min.HasValue)
                        {
                            query = query.Where(p => p.Price >= filterObj.Price.Min);
                        }
                        if (filterObj.Price.Max.HasValue)
                        {
                            query = query.Where(p => p.Price <= filterObj.Price.Max);
                        }
                    }

                }

                if (filterObj.Manufacturers != null && filterObj.Manufacturers.Any())
                {
                    {
                        query = query.Where(p => filterObj.Manufacturers.Contains(p.Manufacturer.Name));
                    }
                }

                if (!String.IsNullOrEmpty(productType))
                {

                    if (filterObj.RAM != null && filterObj.RAM.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Размер оперативной памяти (ГБ)"
                        && filterObj.RAM.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.DriveVolume != null && filterObj.DriveVolume.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Объём накопителя (ГБ)"
                        && filterObj.DriveVolume.Contains(cp.Characteristics.Value)));
                    }
                    if (filterObj.OperatingSystem != null && filterObj.OperatingSystem.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                            cp.Characteristics.CharacteristicsType.Name == "Операционная система" &&
                            filterObj.OperatingSystem.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.CoresQuantity != null && filterObj.CoresQuantity.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Количество ядер"
                        && filterObj.CoresQuantity.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.TabletWidths.Min != null
                        || filterObj.TabletWidths.Max != null)
                    {
                        var tabletsWidths = await _context.Characteristics
                            .Where(c => c.CharacteristicsType.Name == "Рабочая ширина (мм)")
                            .ToListAsync();

                        query = query
                                .Where(p => p.CharacteristicsProduct.Any(cp =>
                                cp.Characteristics.CharacteristicsType.Name == "Рабочая ширина (мм)"));

                        query = GetProductsByFilter("float",tabletsWidths,
                            _context, query,
                            null, null,
                            filterObj.TabletWidths.Min,
                            filterObj.TabletWidths.Max);
                    }

                    if (filterObj.HeadphonesType != null && filterObj.HeadphonesType.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Тип наушников"
                        && filterObj.HeadphonesType.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.AudioScheme != null && filterObj.AudioScheme.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Тип наушников"
                        && filterObj.AudioScheme.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.KeyboardType != null && filterObj.KeyboardType.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Тип клавиатуры"
                        && filterObj.KeyboardType.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.KeysCount.Min != null
                        || filterObj.KeysCount.Max != null)
                    {
                        var keysCount = await _context.Characteristics
                            .Where(c => c.CharacteristicsType.Name == "Количество клавиш")
                            .ToListAsync();

                        query = query
                                .Where(p => p.CharacteristicsProduct.Any(cp =>
                                cp.Characteristics.CharacteristicsType.Name == "Количество клавиш"));

                        query = GetProductsByFilter("int", keysCount,
                            _context, query,
                            filterObj.KeysCount.Min,
                            filterObj.KeysCount.Max,
                            null, null);
                    }

                    if (filterObj.Switches != null && filterObj.Switches.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Свичи"
                        && filterObj.Switches.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.MouseKeysCount.Min != null
                        || filterObj.MouseKeysCount.Max != null)
                    {
                        var keysCount = await _context.Characteristics
                            .Where(c => c.CharacteristicsType.Name == "Количество клавиш")
                            .ToListAsync();

                        query = query
                                .Where(p => p.CharacteristicsProduct.Any(cp =>
                                cp.Characteristics.CharacteristicsType.Name == "Количество клавиш"));

                        query = GetProductsByFilter("int", keysCount,
                            _context, query,
                            filterObj.MouseKeysCount.Min,
                            filterObj.MouseKeysCount.Max,
                            null, null);
                    }

                    if (filterObj.DPI != null && filterObj.DPI.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "DPI"
                        && filterObj.DPI.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.ExecutionType != null && filterObj.ExecutionType.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                            cp.Characteristics.CharacteristicsType.Name == "Вид исполнения"
                            && filterObj.ExecutionType.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.Direction != null && filterObj.Direction.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Направленность"
                        && filterObj.Direction.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.MinFrequency != null && filterObj.MinFrequency.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Минимальная частота (Гц)"
                        && filterObj.MinFrequency.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.MaxFrequency != null && filterObj.MaxFrequency.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Максимальная частота (Гц)"
                        && filterObj.MaxFrequency.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.MonitorFps.Min != null
                        || filterObj.MonitorFps.Max != null)
                    {
                            var monitorFPS = await _context.Characteristics
                                .Where(c => c.CharacteristicsType.Name == "Кадров в секунду")
                                .ToListAsync();

                            query = query
                                .Where(p => p.CharacteristicsProduct.Any(cp =>
                            cp.Characteristics.CharacteristicsType.Name == "Кадров в секунду"));

                        query = GetProductsByFilter("int", monitorFPS,
                                _context, query,
                                filterObj.MonitorFps.Min,
                                filterObj.MonitorFps.Max,
                                null, null);
                        }

                    if (filterObj.Megapixels.Min != null
                        || filterObj.Megapixels.Max != null)
                    {
                        
                        var megapixels = await _context.Characteristics
                                .Where(c => c.CharacteristicsType.Name == "Количество мегапикселей")
                                .ToListAsync();

                        query = query
                            .Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Количество мегапикселей"));

                        query = GetProductsByFilter("float", megapixels,
                            _context, query,
                            null, null,
                            filterObj.Megapixels.Min,
                            filterObj.Megapixels.Max);
                    }

                    if (filterObj.MicrophonePresence != null && filterObj.MicrophonePresence.Any())
                    {
                        if (filterObj.MicrophonePresence.First() == "Есть")
                        {
                            query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Наличие микрофона"
                        && cp.Characteristics.Value == "True"));
                        }
                        if (filterObj.MicrophonePresence.First() == "Нет")
                        {
                            query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Наличие микрофона"
                        && cp.Characteristics.Value == "False"));
                        }

                    }

                    if (filterObj.FPS != null && filterObj.FPS.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Кадров в секунду"
                        && filterObj.FPS.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.Diagonal.Min != null
                        || filterObj.Diagonal.Max != null)
                    {
                        var diagonals = await _context.Characteristics
                                .Where(c => c.CharacteristicsType.Name == "Диагональ (в дюймах)"
                                && c.CharacteristicsProduct.Any(cp=>cp.Products.ProductTypes.Name == productType))
                                .ToListAsync();

                        query = query
                                .Where(p => p.CharacteristicsProduct.Any(cp =>
                            cp.Characteristics.CharacteristicsType.Name == "Диагональ (в дюймах)"
                            && cp.Products.ProductTypes.Name == productType));

                        query = GetProductsByFilter("float", diagonals,
                            _context, query,
                            null, null,
                            filterObj.Diagonal.Min,
                            filterObj.Diagonal.Max);
                    }

                    if (filterObj.MatrixType != null && filterObj.MatrixType.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Тип матрицы"
                        && filterObj.MatrixType.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.AudioConnection != null && filterObj.AudioConnection.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Разъём подключения"
                        && filterObj.AudioConnection.Contains(cp.Characteristics.Value)));
                    }

                    if (filterObj.Connection != null && filterObj.Connection.Any())
                    {
                        query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                        cp.Characteristics.CharacteristicsType.Name == "Тип подключения"
                        && filterObj.Connection.Contains(cp.Characteristics.Value)));
                    }
                }
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
            return View(await PaginatedList<Product>.CreateAsync(query, pageNumber ?? 1, pageSize));
        }

        public IQueryable<Product> GetProductsByFilter(string type,
            List<Characteristics> characteristicsList,
            DigitalDevicesContext _context,
            IQueryable<Product> query,
            int? min, int? max,
            float? minf, float? maxf)
        {
            NumberFormatInfo provider = new()
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = "."
            };
            if (type == "float")
            {
                if (minf.HasValue)
                {
                    List<string> stringValues = new();
                    List<float> floatValues = new();
                    foreach (var characteristic in characteristicsList)
                    {
                        bool v = float.TryParse(characteristic.Value, out float value);
                        if (!v)
                        {
                            value = float.Parse(characteristic.Value, provider);
                        }
                        if (value >= minf)
                        {
                            floatValues.Add(value);
                        }
                    }
                    floatValues.ForEach(v => stringValues.Add(v.ToString()));
                    query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                    stringValues.Contains(cp.Characteristics.Value)));
                }
                if (maxf.HasValue)
                {
                    List<string> stringValues = new();
                    List<float> floatValues = new();
                    foreach (var characteristic in characteristicsList)
                    {
                        bool v = float.TryParse(characteristic.Value, out float value);
                        if (!v)
                        {
                            value = float.Parse(characteristic.Value, provider);
                        }
                        if (value <= maxf)
                        {
                            floatValues.Add(value);
                        }
                    }
                    floatValues.ForEach(v => stringValues.Add(v.ToString()));
                    query = query
                        .Select(p=>p)
                        .Where(p => p.CharacteristicsProduct.Any(cp =>
                    stringValues.Contains(cp.Characteristics.Value)));
                }
            }
            else
            {
                if (min.HasValue)
                {
                    List<string> stringValues = new();
                    List<int> intValues = new();
                    foreach (var characteristic in characteristicsList)
                    {
                        bool v = int.TryParse(characteristic.Value, out int value);
                        if (!v)
                        {
                            value = int.Parse(characteristic.Value, provider);
                        }
                        if (value >= min)
                        {
                            intValues.Add(value);
                        }
                    }
                    intValues.ForEach(v => stringValues.Add(v.ToString()));
                    query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                    stringValues.Contains(cp.Characteristics.Value)));
                }
                if (max.HasValue)
                {
                    List<string> stringValues = new();
                    List<int> intValues = new();
                    foreach (var characteristic in characteristicsList)
                    {
                        bool v = int.TryParse(characteristic.Value, out int value);
                        if (!v)
                        {
                            value = int.Parse(characteristic.Value, provider);
                        }
                        if (value <= max)
                        {
                            intValues.Add(value);
                        }
                    }
                    intValues.ForEach(v => stringValues.Add(v.ToString()));
                    query = query.Where(p => p.CharacteristicsProduct.Any(cp =>
                    stringValues.Contains(cp.Characteristics.Value)));
                }
            }
            return query;
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
        int? pageNumber,
        string filters)
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
            ViewData["FilterList"] = filters;
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
        int? pageNumber,
        string filters)
        {
            ViewData["ProductType"] = productType;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString ?? currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            ViewData["FilterList"] = filters;
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
        int? pageNumber,
        string filters)
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
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index),
                new
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

        // GET: Products/Edit/5
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
            ViewData["SearchString"] = searchString ??= currentFilter;
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortField"] = sortField;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            ViewData["FilterList"] = filters;
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
        int? pageNumber,
        string filters)

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
                    pageNumber,
                    filters
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
        int? pageNumber,
        string filters)
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
            ViewData["FilterList"] = filters;
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
        int? pageNumber,
        string filters)
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
                filters
            });
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
