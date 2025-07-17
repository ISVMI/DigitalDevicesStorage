using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.ManufacturersService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturersService _service;

        public ManufacturersController(IManufacturersService service)
        {
            _service = service;
        }

        // GET: Manufacturers
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var manufacturers = await _service.GetAllAsync(token);

            return Ok(manufacturers);
        }

        // GET: Manufacturers/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            Console.WriteLine("--> Front end needed");
            return Ok();
        }

        // POST: Manufacturers/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateManufacturerDto manufacturerDto, CancellationToken token)
        {
            try
            {

                var manufacturer = await _service.CreateAsync(manufacturerDto, token);

                return CreatedAtAction(nameof(Index), manufacturer);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { manufacturerDto, message });
            }

        }

        // GET: Manufacturers/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            try
            {
                var manufacturer = await _service.GetByIdAsync(id, token);
                return Ok(new { id, manufacturer });
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return NotFound();
            }

        }

        // POST: Manufacturers/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(int id, EditManufacturerDto manufacturerDto, CancellationToken token)
        {
            if (id != manufacturerDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateAsync(manufacturerDto, token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit manufacturer {manufacturerDto.Name}: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(manufacturerDto);
        }

        // GET: Manufacturers/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                var manufacturer = await _service.GetByIdAsync(id, token);

                return Ok(manufacturer);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }
        }

        // POST: Manufacturers/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            if (!_service.GetAllAsync(token).Result.Any())
            {
                return Problem("--> Db 'Manufacturers' was null.");
            }

            var result = await _service.DeleteAsync(id, token);

            if (result == false)
            {
                var message = $"--> Couldn't delete manufacturer with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
