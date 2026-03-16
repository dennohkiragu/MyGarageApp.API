using System.Security.Claims;
using GarageApp.API.DTOs;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);

            if (result != "User registered successfully.")
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _authService.LoginAsync(model);

            if (result == "Invalid email or password.")
            {
                return Unauthorized(new { message = result });
            }

            return Ok(new { token = result });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var user = await _authService.GetCurrentUserAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }
    }
}