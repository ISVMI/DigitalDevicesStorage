using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using DigitalDevices.ProductTypesService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ProductTypesService.Infrastructure.Repositories
{
    public class ProductTypesRepo : IProductTypesRepo
    {
        private readonly ProductTypesContext _context;

        public ProductTypesRepo(ProductTypesContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(ProductTypes productType, CancellationToken token = default)
        {
            if (productType == null)
            {
                throw new ArgumentNullException(nameof(productType), "Given product type was null!");
            }

            var existingProductType = await _context.ProductTypes
                .Select(pt => pt)
                .Where(pt => pt.Name.Contains($"{productType.Name}")).FirstOrDefaultAsync(token);

            if (existingProductType != null)
            {
                throw new Exception("Such product type already exists!");
            }

            await _context.ProductTypes.AddAsync(productType,token);

            await _context.SaveChangesAsync(token);
            return productType.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var productTypeToDelete = await GetByIdAsync(id, token);

                _context.ProductTypes.Remove(productTypeToDelete);

                await _context.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't delete characteristic with the given id {id} : {ex.Message}");
                return false;
            }
        }

        public async Task<ProductTypes> UpdateAsync(ProductTypes productType, CancellationToken token = default)
        {
            try
            {
                var existingProductType= await GetByIdAsync(productType.Id, token);

                _context.Entry(existingProductType).CurrentValues.SetValues(productType);

                await _context.SaveChangesAsync(token);
                return existingProductType;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update product type", ex);
            }
        }

        public async Task<ProductTypes> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var productTypeToFind = await _context.ProductTypes.FindAsync(id, token);

            if (productTypeToFind == null)
            {
                throw new Exception($"Product type with id: {id} not found");
            }

            return productTypeToFind;
        }

        public async Task<IEnumerable<ProductTypes>> GetAllAsync(CancellationToken token = default)
        {
            var productTypes = await _context.ProductTypes.ToListAsync(token);

            return productTypes;
        }
    }
}
