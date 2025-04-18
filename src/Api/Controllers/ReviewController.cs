using Application.Requests.ReviewRequests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequest review)
        {
            var reviewId = await _reviewService.Create(review);
            var result = new { Id = reviewId };
            return CreatedAtAction(nameof(ReadById), result, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _reviewService.ReadAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _reviewService.ReadById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReviewRequest review)
        {
            await _reviewService.Update(review);
            return NoContent();
        }
    }
}
