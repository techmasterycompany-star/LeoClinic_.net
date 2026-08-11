using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IRatingService _ratingService;
        public PatientController(IPatientService patientService, IAppointmentService appointmentService, IRatingService ratingService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _ratingService = ratingService;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetPatientProfile()
        {
            var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var patient = await _patientService.GetPatientByIdAsync(patientId);
            if (patient is null) return NotFound();

            return Ok(patient);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientProfileDTO dto)
        {
            if (dto is null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var result = await _patientService.UpdatePatientProfileAsync(id, dto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpGet("/appointments")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAppointmentsByPatient()
        {
            try
            {
                var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var appointments = await _appointmentService.GetAllByPatientAsync(patientId);
                return Ok(appointments);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost("/review")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateReview([FromBody] CreateRatingDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                var result = await _ratingService.CreateRating(patientId, dto);
                return CreatedAtAction("GetReviewById", "Rating", new { id = result.Id }, result);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "A database error occurred." });
            }
            catch
            {
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPut("/review/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateRatingDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


            try
            {
                var result = await _ratingService.UpdateRating(id, patientId, dto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch (UnauthorizedAccessException ua)
            {
                return Forbid(ua.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpDelete("/review/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var result = await _ratingService.DeleteRating(id,patientId);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch (UnauthorizedAccessException ua)
            {
                return Forbid(ua.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost("appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDTO appointment)
        {
            if (appointment is null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var app = await _appointmentService.BookAppointmentAsync(patientId, appointment);
                return Ok(app);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { error = inv.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPut("appointment/{id}/reschedule/{availabilityId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> RescheduleAppointment(int id, int availabilityId)
        {
            try
            {
                var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var result = await _appointmentService.RescheduleAppointment(id, availabilityId, patientId);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
            catch(UnauthorizedAccessException ua)
            {
                return Forbid(ua.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPut("appointment/{id}/cancel")]
        [Authorize(Roles = "Patient, Admin")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
                if(!ModelState.IsValid)
                    return ValidationProblem(ModelState);
            try
            {
                int? patientId = null;
                if (User.IsInRole("Patient"))
                {
                    patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                }

                var result = await _appointmentService.UpdateAppointmentStatusAsync(id, Domain.Enums.AppointmentStatus.Cancelled, patientId);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException ua)
            {
                return Forbid(ua.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }
    }
}
