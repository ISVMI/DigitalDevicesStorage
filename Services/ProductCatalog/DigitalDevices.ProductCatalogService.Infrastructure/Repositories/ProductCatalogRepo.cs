using DigitalDevices.ProductCatalogService.Core.Interfaces;
using DigitalDevices.ProductCatalogService.Core.Models;
using DigitalDevices.ProductCatalogService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Repositories
{
    public class ProductCatalogRepo : IProductCatalogRepo
    {
        private readonly ProductCatalogContext _context;

        public ProductCatalogRepo(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Product product, CancellationToken token = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Given product was null!");
            }

            var existingProductType = await _context.Products
                .Select(pt => pt)
                .Where(pt => pt.Name.Contains($"{product.Name}")).FirstOrDefaultAsync(token);

            if (existingProductType != null)
            {
                throw new AlreadyExistsException("Such product already exists!");
            }

            await _context.Products.AddAsync(product, token);

            await _context.SaveChangesAsync(token);
            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var productToDelete = await GetByIdAsync(id, token);

                _context.Products.Remove(productToDelete);

                await _context.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't delete product with the given id {id} : {ex.Message}");
                return false;
            }
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken token = default)
        {
            try
            {
                var existingProductType = await GetByIdAsync(product.Id, token);

                _context.Entry(existingProductType).CurrentValues.SetValues(product);

                await _context.SaveChangesAsync(token);
                return existingProductType;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update product", ex);
            }
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var productTypeToFind = await _context.Products.FindAsync(id, token);

            if (productTypeToFind == null)
            {
                throw new NotFoundException($"Product with id: {id} not found");
            }

            return productTypeToFind;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken token = default)
        {
            var productTypes = await _context.Products.ToListAsync(token);

            return productTypes;
        }

        public async Task<(IEnumerable<Product>, int)> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            var query = _context.Products.AsNoTracking();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var totalCount = await query.CountAsync(token);

            return (items, totalCount);
        }
    }
}
