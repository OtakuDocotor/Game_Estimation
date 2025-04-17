using Application.Requests.UserRequest;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest user)
        {
            var userId = await _userService.Create(user);
            var result = new { id = userId };
            return CreatedAtAction(nameof(ReadById), result, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _userService.ReadAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _userService.ReadById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest user)
        {
            await _userService.Update(user);
            return NoContent();
        }
    }
}
