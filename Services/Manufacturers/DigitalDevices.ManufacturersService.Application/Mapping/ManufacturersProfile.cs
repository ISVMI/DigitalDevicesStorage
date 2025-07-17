using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Core.Models;

namespace DigitalDevices.ManufacturersService.Application.Mapping
{
    public class ManufacturersProfile : Profile
    {
        public ManufacturersProfile()
        {
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<CreateManufacturerDto, Manufacturer>();
            CreateMap<EditManufacturerDto, Manufacturer>();
        }
    }
}
