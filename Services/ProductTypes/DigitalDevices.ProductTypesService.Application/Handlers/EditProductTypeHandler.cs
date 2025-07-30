using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class EditProductTypeHandler : IRequestHandler<EditProductTypeCommand, ProductTypeDto>
    {
        private readonly IProductTypesRepo _repo;
        private readonly IMapper _mapper;

        public EditProductTypeHandler(IProductTypesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductTypeDto> Handle(EditProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = _mapper.Map<ProductTypes>(request.ProductTypeToEdit);
            await _repo.UpdateAsync(productType, cancellationToken);
            return _mapper.Map<ProductTypeDto>(productType);
        }
    }
}
