using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, Guid>
    {
        private readonly IProductTypesRepo _repo;
        private readonly IMapper _mapper;

        public CreateProductTypeHandler(IProductTypesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = _mapper.Map<ProductTypes>(request.ProductType);
            await _repo.CreateAsync(productType, cancellationToken);
            return productType.Id;
        }
    }
}
