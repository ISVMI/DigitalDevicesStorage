using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.CharacteristicsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacteristicsController : ControllerBase
    {
        private readonly ICharacteristicsService _service;

        public CharacteristicsController(ICharacteristicsService service)
        {
            _service = service;
        }

        // GET: Characteristics
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var characteristics = await _service.GetAllAsync(token);

            return Ok(characteristics);
        }

        // GET: Characteristics/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: Characteristics/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCharacteristicDto createCharacteristicDto, CancellationToken token)
        {
            try
            {
                var characteristic = await _service.CreateAsync(createCharacteristicDto, token);

                return CreatedAtAction(nameof(Index), characteristic);
            }
            catch (Exception ex)
            {
                var message = $"--> Couldn't add a new characteristic: {ex.Message}";
                Console.WriteLine(message);
                return BadRequest(new {createCharacteristicDto, message });
            }

        }

        // GET: Characteristics/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            try 
            {
                var characteristic = await _service.GetByIdAsync(id, token);

                return Ok(new { id, characteristic });
            }
            catch(Exception ex)
            {
                var message = $"--> Couldn't find characteristic: {ex.Message}";
                Console.WriteLine(message);
                return NotFound();
            }
        }

        // POST: Characteristics/Edit/5
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(int id, EditCharacteristicDto editCharacteristicDto, CancellationToken token)
        {
            if (id != editCharacteristicDto.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateAsync(editCharacteristicDto, token);
            }
            catch(Exception ex)
            {
                var message = $"--> Couldn't edit characteristic: {ex.Message} / {ex.InnerException.Message}";
                Console.WriteLine(message);
                return NotFound(new { id, message });
            }

            return Ok(editCharacteristicDto);
        }

        // GET: Characteristics/Delete/5
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                var characteristicToDelete = await _service.GetByIdAsync(id, token);

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
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            if (!_service.GetAllAsync(token).Result.Any())
            {
                return Problem("--> Db 'Characteristics' was null.");
            }

            var result = await _service.DeleteAsync(id, token);

            if (result == false)
            {
                var message = $"--> Couldn't delete characteristic with id: {id}";
                return NotFound(new { message });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
