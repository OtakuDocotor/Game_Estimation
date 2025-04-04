using Application.DTO;
using Application.Requests.GameRequests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult>Create([FromBody] CreateGameRequest game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gameId = await _gameService.Create(game);
            var result = new { id = gameId };
            return CreatedAtAction(nameof(ReadById), new { id = gameId }, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _gameService.Delete(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _gameService.ReadAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _gameService.ReadById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGameRequest game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _gameService.Update(game);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
