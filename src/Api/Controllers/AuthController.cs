using Application.DTO;
using Application.Requests;
using Application.Requests.UserRequests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [EnableRateLimiting("auth")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest user)
        {
            await authService.Register(user);
            return Created();
        }

        [EnableRateLimiting("auth")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest user)
        {
            var response = await authService.Login(user);
            return Ok(response);
        }
    }
}
