using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Commands;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductCatalogRepo _repo;

        public DeleteProductHandler(IProductCatalogRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
