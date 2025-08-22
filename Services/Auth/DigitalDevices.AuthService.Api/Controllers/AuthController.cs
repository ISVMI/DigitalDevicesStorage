using DigitalDevices.AuthService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace DigitalDevices.AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(
            IUsersService usersService)
        {
            _usersService = usersService;
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
                HttpContext.Response.Cookies.Append("some-cookies", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not login: {ex.Message}");
                return Unauthorized();
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> Me()
        {
            return Ok(new { userId = HttpContext.Request.Headers["X-User-Id"],
                role = HttpContext.Request.Headers["X-User-Roles"],
                permissionLevel = HttpContext.Request.Headers["X-Permission-Level"]});
        }
    }
}
