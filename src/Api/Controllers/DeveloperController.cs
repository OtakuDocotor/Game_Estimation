using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Services;
using Application.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Requests.DeveloperRequests;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeveloperRequest developer)
        {
            var developerId = await _developerService.Create(developer);
            var result = new { Id = developerId };
            return CreatedAtAction(nameof(ReadById), result, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _developerService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _developerService.ReadAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _developerService.ReadById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDeveloperRequest developer)
        {
            await _developerService.Update(developer);
            return NoContent();
        }
    }
}
