using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.CharacteristicsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacteristicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacteristicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Characteristics/All
        [HttpGet("All")]
        public async Task<IActionResult> All(CancellationToken token)
        {
            var characteristics = await _mediator.Send(new GetAllCharacteristicsQuery(), token);

            return Ok(characteristics);
        }

        // GET: Characteristics/Paged
        [HttpGet("Paged")]
        public async Task<IActionResult> Paged(CancellationToken token)
        {
            var characteristics = await _mediator.Send(new GetAllCharacteristicsPagedQuery(), token);

            return Ok(characteristics.Items);
        }

        // GET: Characteristics/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: Characteristics/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCharacteristicCommand command, CancellationToken token)
        {
            try
            {
                var characteristicId = await _mediator.Send(command, token);

                return CreatedAtAction(nameof(Index), new { characteristicId }, command);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new characteristic: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { command, message });
            }

        }

        [HttpGet("GetCharacteristicsByProductTypeId/{id}")]
        public async Task<IActionResult> GetCharacteristicsByProductTypeId(Guid id, CancellationToken token)
        {
            try
            {
                var characteristics = await _mediator.Send(new GetCharacteristicsByProductTypeIdQuery(id), token);

                return Ok(characteristics);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't get characteristics by product type id {id}: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { message });
            }

        }

        // GET: Characteristics/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token)
        {
            try
            {
                var characteristic = await _mediator.Send(new GetCharacteristicQuery(id), token);

                return Ok(new { id, characteristic });
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find characteristic: {ex.Message}";
                Console.WriteLine(message);
                return NotFound();
            }
        }

        // POST: Characteristics/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid id, EditCharacteristicDto editCharacteristicDto, CancellationToken token)
        {
            if (id != editCharacteristicDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new EditCharacteristicCommand(editCharacteristicDto), token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit characteristic: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(editCharacteristicDto);
        }

        // GET: Characteristics/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var characteristicToDelete = await _mediator.Send(new GetCharacteristicQuery(id), token);

                return Ok(characteristicToDelete);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find characteristic: {ex.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }
        }

        // POST: Characteristics/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken token)
        {
            var characteristics = await _mediator.Send(new GetAllCharacteristicsQuery(), token);
            if (!characteristics.Any())
            {
                return Problem("--> Db 'Characteristics' was null.");
            }

            var result = await _mediator.Send(new DeleteCharacteristicCommand(id), token);

            if (result == false)
            {
                var message = $"--> Couldn't delete characteristic with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
