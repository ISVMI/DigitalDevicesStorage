using DigitalDevices.ProductCatalogService.Core.Models;
using Shared.Interfaces;

namespace DigitalDevices.ProductCatalogService.Core.Interfaces
{
    public interface IProductCatalogRepo : IRepository<Product>
    {
    }
}
