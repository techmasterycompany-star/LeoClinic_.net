using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        //get rating by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _ratingService.GetRatingByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        

        //update rating
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateRatingDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _ratingService.UpdateRating(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        //delete rating
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _ratingService.DeleteRating(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}