using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Services;
using Application.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public async Task<IActionResult> Create([FromBody] DeveloperDTO developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var developerId = await _developerService.Create(developer);
            var result = new { Id = developerId };
            return CreatedAtAction(nameof(ReadById), new { id = developerId }, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _developerService.Delete(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
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
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DeveloperDTO developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _developerService.Update(developer);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
