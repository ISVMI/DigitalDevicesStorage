using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using DigitalDevices.ManufacturersService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.ManufacturersService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManufacturersController(IManufacturersService service, IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Manufacturers
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var manufacturers = await _mediator.Send(new GetAllManufacturersQuery(), token);

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
        [HttpPost("AddManufacturer")]
        public async Task<IActionResult> AddManufacturer([FromBody] CreateManufacturerCommand command, CancellationToken token)
        {
            var id = await _mediator.Send(command, token);
            return CreatedAtAction(nameof(GetManufacturerById), new {id}, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturerById(Guid id, CancellationToken token)
        {
            var manufacturer = await _mediator.Send(new GetManufacturerQuery(id), token);
            return Ok(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token)
        {
            try
            {
                var manufacturer = await _mediator.Send(new GetManufacturerQuery(id), token);
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
        [HttpPost("EditManufacturer{id}")]
        public async Task<IActionResult> EditManufacturer(Guid id, EditManufacturerDto manufacturerDto, CancellationToken token)
        {
            if (id != manufacturerDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new EditManufacturerCommand(manufacturerDto), token);
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
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var manufacturer = await _mediator.Send(new GetManufacturerQuery(id), token);

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
        [HttpPost("DeleteManufacturer{id}")]
        public async Task<IActionResult> DeleteManufacturer(Guid id, CancellationToken token)
        {
            var manufacturers = await _mediator.Send(new GetAllManufacturersQuery(), token);
            if (!manufacturers.Any())
            {
                return Problem("--> Db 'Manufacturers' was null.");
            }

            var result = await _mediator.Send(new DeleteManufacturerCommand(id), token);

            if (result)
            {
                return NoContent();
            }
            var message = $"--> Couldn't delete manufacturer with id: {id}";
            return NotFound(new { message });

        }
    }
}
