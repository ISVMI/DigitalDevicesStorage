using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Application.Mapping
{
    public class CharacteristicsProfile : Profile
    {
        public CharacteristicsProfile()
        {
            CreateMap<Characteristics, CharacteristicDto>();
            CreateMap<CreateCharacteristicDto, Characteristics>();
            CreateMap<EditCharacteristicDto, Characteristics>();
            CreateMap<CharacteristicsType, CharacteristicTypeDto>();
            CreateMap<CreateCharacteristicTypeDto, CharacteristicsType>();
            CreateMap<EditCharacteristicTypeDto, CharacteristicsType>();
        }
    }
}
