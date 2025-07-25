using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.CharacteristicsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacteristicsTypesController : ControllerBase
    {
        private readonly ICharacteristicsTypeService _service;

        public CharacteristicsTypesController(ICharacteristicsTypeService service)
        {
            _service = service;
        }

        // GET: CharacteristicsTypes
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var characteristicsType = await _service.GetAllAsync(token);

            return Ok(characteristicsType);
        }

        // GET: CharacteristicsTypes/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: CharacteristicsTypes/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCharacteristicTypeDto createCharacteristicTypeDto,
            CancellationToken token)
        {
            try
            {
                var characteristicType = await _service.CreateAsync(createCharacteristicTypeDto, token);

                return CreatedAtAction(nameof(Index), characteristicType);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new { createCharacteristicTypeDto, message });
            }

        }

        // GET: CharacteristicsTypes/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken token)
        {
            var characteristicType = await _service.GetByIdAsync(id, token);

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
                await _service.UpdateAsync(editCharacteristicTypeDto, token);
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
                var characteristicTypeToDelete = await _service.GetByIdAsync(id, token);

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
            if (!_service.GetAllAsync(token).Result.Any())
            {
                return Problem("--> Db 'Characteristics' was null.");
            }

            var result = await _service.DeleteAsync(id, token);

            if (result == false)
            {
                var message = $"--> Couldn't delete characteristic type with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
