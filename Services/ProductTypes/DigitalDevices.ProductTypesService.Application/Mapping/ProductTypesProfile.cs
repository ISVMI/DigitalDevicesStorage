using AutoMapper;
using DigitalDevices.ProductTypesService.Core.Models;
using DigitalDevices.ProductTypesService.Application.Dtos;

namespace DigitalDevices.ProductTypesService.Application.Mapping
{
    public class ProductTypesProfile : Profile
    {
        public ProductTypesProfile()
        {
            CreateMap<ProductTypes, ProductTypeDto>();
            CreateMap<CreateProductTypeDto, ProductTypes>();
            CreateMap<EditProductTypeDto, ProductTypes>();
        }

    }
}
