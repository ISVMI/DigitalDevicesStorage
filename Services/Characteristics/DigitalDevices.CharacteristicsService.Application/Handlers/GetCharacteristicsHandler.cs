using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicsHandler : IRequestHandler<GetAllCharacteristicsPagedQuery, PagedResponse<CharacteristicDto>>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public GetCharacteristicsHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<CharacteristicDto>> Handle(GetAllCharacteristicsPagedQuery request, CancellationToken token)
        {
            var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize, token);

            return new PagedResponse<CharacteristicDto>(_mapper.Map<IEnumerable<CharacteristicDto>>(items), totalCount, request.Page, request.PageSize);
        }
    }
}
