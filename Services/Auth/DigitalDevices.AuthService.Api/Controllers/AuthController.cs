using DigitalDevices.AuthService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace DigitalDevices.AuthService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repo;
        private readonly IUsersService _usersService;

        public AuthController(
            IAuthRepo repo,
            IUsersService usersService)
        {
            _repo = repo;
            _usersService = usersService;
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            return Ok(await _repo.GetRoles());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistration userReg)
        {
            if (userReg == null)
            {
                Console.WriteLine("--> User was null");

                return BadRequest();
            }

            await _usersService.Register(userReg.Username, userReg.Password, userReg.SecretCode);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            try
            {
                var token = await _usersService.Login(user.Username, user.Password);
                HttpContext.Response.Cookies.Append("some-cookies", token);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not login: {ex.Message}");
                return Unauthorized();
            }

            return Ok();
        }

        [Authorize("ManagerPolicy")]
        [HttpGet("Secret")]
        public ActionResult Secret()
        {
            return Ok();
        }
    }
}
