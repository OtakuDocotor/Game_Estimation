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
            var gameId = await _gameService.Create(game);
            var result = new { id = gameId };
            return CreatedAtAction(nameof(ReadById), result, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _gameService.Delete(id);
            return NoContent();
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
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGameRequest game)
        {
            await _gameService.Update(game);
            return NoContent();
        }
    }
}
