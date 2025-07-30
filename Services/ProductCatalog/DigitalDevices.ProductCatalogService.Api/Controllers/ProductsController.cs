using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Commands;
using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Interfaces;
using DigitalDevices.ProductCatalogService.Application.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Messages;

namespace DigitalDevices.ProductCatalogService.Api.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IManufacturersClient _manufacturersClient;
        private readonly IProductTypesClient _productTypesClient;
        private readonly ICharacteristicsClient _characteristicsClient;

        public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger, IManufacturersClient manufacturersClient,
            IProductTypesClient productTypesClient, ICharacteristicsClient characteristicsClient)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _manufacturersClient = manufacturersClient;
            _productTypesClient = productTypesClient;
            _characteristicsClient = characteristicsClient;
        }

        // GET: api/Products/Manufacturers
        [HttpGet("Manufacturers")]
        public async Task<IActionResult> Manufacturers(CancellationToken token = default)
        {
            var manufacturers = await _manufacturersClient.GetAllAsync(token);

            return Ok(manufacturers);
        }

        // GET: api/Products/Characteristics/id
        [HttpGet("Characteristics")]
        public async Task<IActionResult> Characteristics(Guid id, CancellationToken token = default)
        {
            var characteristics = await _characteristicsClient.GetCharacteristicsByProductTypeId(id, token);

            return Ok(characteristics);
        }

        // GET: api/Products/ProductTypes
        [HttpGet("ProductTypes")]
        public async Task<IActionResult> ProductTypes(CancellationToken token = default)
        {
            var productTypes = await _productTypesClient.GetAllAsync(token);

            return Ok(productTypes);
        }

        // GET: api/Products/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts(CancellationToken token = default)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(), token);

            return Ok(products);
        }

        // GET: api/Products/Paged
        [HttpGet("Paged")]
        public async Task<IActionResult> GetProductsPaged(CancellationToken token = default)
        {
            var productsPaged = await _mediator.Send(new GetAllProductsPagedQuery(), token);

            return Ok(productsPaged.Items);
        }

        // GET: api/Products/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: api/Products/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command, IBus bus,
            CancellationToken token = default)
        {
            try
            {
                var productId = await _mediator.Send(command, token);

                var characteristics = _mapper.Map<List<CharacteristicMessageDto>>(command.Product.Characteristics);

                var newProductTypeCreatedMessage = new ProductCreated(productId, characteristics);
                await bus.Publish(newProductTypeCreatedMessage, token);

                return CreatedAtAction(nameof(GetProductsPaged), productId);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new product: {ex.Message}";
                _logger.LogError(message);
                return BadRequest(new { command, message });
            }
        }

        // GET: api/Products/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token = default)
        {
            try
            {
                var product = await _mediator.Send(new GetProductQuery(id), token);

                return Ok(new { id, product });
            }
            catch (Exception ex)
            {
                var message = $"Couldn't find product: {ex.Message}";
                _logger.LogError(message);
                return NotFound();
            }
        }

        // POST: api/Products/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid id, EditProductDto editProductDto,
            CancellationToken token = default)
        {
            if (id != editProductDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new EditProductCommand(editProductDto), token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit product: {ex.Message} / {ex.InnerException.Message}";
                _logger.LogError(message);
                return NotFound(new { id, message });
            }

            return Ok(editProductDto);
        }


        // GET: api/Products/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
        {
            try
            {
                var productToDelete = await _mediator.Send(new GetProductQuery(id), token);

                return Ok(productToDelete);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find product: {ex.Message}";
                _logger.LogError(message);
                return NotFound(new { id, message });
            }
        }

        // POST: api/Products/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken token = default)
        {

            var products = await _mediator.Send(new GetAllProductsQuery(), token);

            if (!products.Any())
            {
                return Problem("--> Db 'Products' was null.");
            }

            var result = await _mediator.Send(new DeleteProductCommand(id), token);

            if (result == false)
            {
                var message = $"--> Couldn't delete product with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
