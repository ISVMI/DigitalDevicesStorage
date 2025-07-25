using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;

namespace DigitalDevices.ProductTypesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypesService _service;

        public ProductTypesController(IProductTypesService service)
        {
            _service = service;
        }

        // GET: ProductTypesController
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            var productTypes = await _service.GetAllAsync(token);

            return Ok(productTypes);
        }

        // GET: ProductTypesController/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: ProductTypesController/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductTypeDto createProductTypeDto, IBus bus, CancellationToken token = default)
        {
            try
            {
                var productTypeId = await _service.CreateAsync(createProductTypeDto, token);

                var newProductTypeAddedMessage = new NewProductTypeAdded(productTypeId, createProductTypeDto.CharacteristicsTypesIds);
                await bus.Publish(newProductTypeAddedMessage, token);

                return CreatedAtAction(nameof(Index), productTypeId);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new product type: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { createProductTypeDto, message });
            }
        }

        // GET: ProductTypesController/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token = default)
        {
            try
            {
                var productType = await _service.GetByIdAsync(id, token);

                return Ok(new { id, productType });
            }
            catch (Exception ex)
            {
                var message = $"Couldn't find product type: {ex.Message}";
                Console.WriteLine(message);
                return NotFound();
            }
        }

        // POST: ProductTypesController/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid id, EditProductTypeDto editProductTypeDto, CancellationToken token = default)
        {
            if (id != editProductTypeDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateAsync(editProductTypeDto, token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit product type: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(editProductTypeDto);
        }


        // GET: ProductTypesController/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
        {
            try
            {
                var productTypeToDelete = await _service.GetByIdAsync(id, token);

                return Ok(productTypeToDelete);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find product type: {ex.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }
        }

        // POST: ProductTypesController/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken token = default)
        {
            if (!_service.GetAllAsync(token).Result.Any())
            {
                return Problem("--> Db 'Product types' was null.");
            }

            var result = await _service.DeleteAsync(id, token);

            if (result == false)
            {
                var message = $"--> Couldn't delete product type with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
