using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;

namespace DigitalDevices.ProductTypesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ProductTypes/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProductTypes(CancellationToken token = default)
        {
            var productTypes = await _mediator.Send(new GetAllProductTypesPagedQuery(),token);

            return Ok(productTypes.Items);
        }

        // GET: api/ProductTypes/Paged
        [HttpGet("Paged")]
        public async Task<IActionResult> GetProductTypesPaged(CancellationToken token = default)
        {
            var productTypes = await _mediator.Send(new GetAllProductTypesPagedQuery(), token);

            return Ok(productTypes.Items);
        }

        // GET: api/ProductTypes/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: api/ProductTypes/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateProductTypeCommand command, IBus bus, CancellationToken token = default)
        {
            try
            {
                var productTypeId = await _mediator.Send(command,token);

                var newProductTypeAddedMessage = new ProductTypeCreated(productTypeId, command.ProductType.CharacteristicsTypesIds);
                await bus.Publish(newProductTypeAddedMessage, token);

                return CreatedAtAction(nameof(Index), productTypeId);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new product type: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { command, message });
            }
        }

        // GET: api/ProductTypes/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token = default)
        {
            try
            {
                var productType = await _mediator.Send(new GetProductTypeQuery(id),token);

                return Ok(new { id, productType });
            }
            catch (Exception ex)
            {
                var message = $"Couldn't find product type: {ex.Message}";
                Console.WriteLine(message);
                return NotFound();
            }
        }

        // POST: api/ProductTypes/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid id, EditProductTypeDto editProductTypeDto, CancellationToken token = default)
        {
            if (id != editProductTypeDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new EditProductTypeCommand(editProductTypeDto), token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit product type: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(editProductTypeDto);
        }


        // GET: api/ProductTypes/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
        {
            try
            {
                var productTypeToDelete = await _mediator.Send(new GetProductTypeQuery(id),token);

                return Ok(productTypeToDelete);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find product type: {ex.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }
        }

        // POST: api/ProductTypes/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken token = default)
        {

            var productTypes = await _mediator.Send(new GetAllProductTypesQuery(), token);

            if (!productTypes.Any())
            {
                return Problem("--> Db 'Product types' was null.");
            }

            var result = await _mediator.Send(new DeleteProductTypeCommand(id), token);

            if (result == false)
            {
                var message = $"--> Couldn't delete product type with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
