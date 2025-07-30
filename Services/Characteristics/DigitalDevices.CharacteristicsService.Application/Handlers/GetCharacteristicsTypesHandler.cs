using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicsTypesHandler : IRequestHandler<GetAllCharacteristicsTypesPagedQuery, PagedResponse<CharacteristicTypeDto>>
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public GetCharacteristicsTypesHandler(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<CharacteristicTypeDto>> Handle(GetAllCharacteristicsTypesPagedQuery request, CancellationToken token)
        {
            var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize, token);

            return new PagedResponse<CharacteristicTypeDto>(_mapper.Map<IEnumerable<CharacteristicTypeDto>>(items), totalCount, request.Page, request.PageSize);
        }
    }
}
