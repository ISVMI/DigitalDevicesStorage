using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Queries;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetManufacturersHandler : IRequestHandler<GetAllManufacturersPagedQuery, PagedResponse<ManufacturerDto>>
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public GetManufacturersHandler(IManufacturersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<ManufacturerDto>> Handle(GetAllManufacturersPagedQuery request, CancellationToken token)
        {
            var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize, token);

            return new PagedResponse<ManufacturerDto>(_mapper.Map<IEnumerable<ManufacturerDto>>(items), totalCount, request.Page, request.PageSize);
        }
    }
}
