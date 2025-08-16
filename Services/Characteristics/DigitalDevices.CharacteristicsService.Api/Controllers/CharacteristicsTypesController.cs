using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.CharacteristicsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacteristicsTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacteristicsTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: CharacteristicsTypes/All
        [HttpGet("All")]
        public async Task<IActionResult> All(CancellationToken token)
        {
            var characteristicsTypes = await _mediator.Send(new GetAllCharacteristicTypesQuery(),token);

            return Ok(characteristicsTypes);
        }

        // GET: CharacteristicsTypes/Paged
        [HttpGet("Paged")]
        public async Task<IActionResult> Paged(CancellationToken token)
        {
            var characteristicsTypes = await _mediator.Send(new GetAllCharacteristicsTypesPagedQuery(), token);

            return Ok(characteristicsTypes.Items);
        }

        // GET: CharacteristicsTypes/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: CharacteristicsTypes/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCharacteristicTypeCommand command, CancellationToken token)
        {
            try
            {
                var characteristicTypeId = await _mediator.Send(command, token);

                return CreatedAtAction(nameof(Index), new {characteristicTypeId, command});
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { command, message });
            }

        }

        // GET: CharacteristicsTypes/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token)
        {
            var characteristicType = await _mediator.Send(new GetCharacteristicTypeQuery(id), token);

            if (characteristicType == null)
            {
                return NotFound();
            }

            return Ok(new { id, characteristicType });
        }

        // POST: CharacteristicsTypes/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid id, EditCharacteristicTypeDto editCharacteristicTypeDto,
            CancellationToken token)
        {
            if (id != editCharacteristicTypeDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new EditCharacteristicTypeCommand(editCharacteristicTypeDto), token);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't edit characteristic type: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(editCharacteristicTypeDto);
        }

        // GET: CharacteristicsTypes/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var characteristicTypeToDelete = await _mediator.Send(new GetCharacteristicTypeQuery(id), token);

                return Ok(characteristicTypeToDelete);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't find characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }
        }

        // POST: CharacteristicsTypes/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken token)
        {
            var characteristicTypes = await _mediator.Send(new GetAllCharacteristicTypesQuery(), token);

            if (!characteristicTypes.Any())
            {
                return Problem("--> Db 'Characteristics' was null.");
            }

            var result = await _mediator.Send(new DeleteCharacteristicTypeCommand(id), token);

            if (result == false)
            {
                var message = $"--> Couldn't delete characteristic type with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
