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

        [HttpPost("{doctorId}")]
        public async Task<IActionResult> AddReview(int doctorId, [FromBody] CreateRatingDTO dto)
        {
            try
            {
                var result = await _ratingService.CreateRating(dto);
                return CreatedAtAction(nameof(GetReviewById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //update rating
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateRatingDTO dto)
        {
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

        //get rating by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _ratingService.GetRatingByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}