using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductTypesRepo _repo;

        public DeleteProductTypeHandler(IProductTypesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
