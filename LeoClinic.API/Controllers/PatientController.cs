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

        private async Task<int?> GetCurrentPatientIdAsync()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var patientId = await _patientService.GetPatientIdByUserIdAsync(userId);
            return patientId > 0 ? patientId : null;
        }


        [HttpGet("")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetPatientProfile()
        {
            var patientId =  await GetCurrentPatientIdAsync();
            if (patientId is null)
                return NotFound(new { error = "Patient profile not found." });

            var patient = await _patientService.GetPatientByIdAsync(patientId.Value);
            if (patient is null) return NotFound(new { error = "Patient profile not found." });

            return Ok(patient);
        }


        [HttpPut("")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientProfileDTO dto)
        {
            if (dto is null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var patientId = await GetCurrentPatientIdAsync();
            if (patientId is null)
                return NotFound(new { error = "Patient profile not found." });

            try
            {
                var result = await _patientService.UpdatePatientProfileAsync(patientId.Value, dto);
                if (!result)
                    return NotFound(new { error = "Patient Profile Not Found" });

                return Ok(new { message = "Patient profile updated successfully." });
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

        [HttpGet("appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAppointmentsByPatient()
        {
            try
            {
                var patientId = await GetCurrentPatientIdAsync();
                if (patientId is null)
                    return NotFound(new { error = "Patient profile not found." });

                var appointments = await _appointmentService.GetAllByPatientAsync(patientId.Value);
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
                var patientId = await GetCurrentPatientIdAsync();
                if (patientId is null)
                    return NotFound(new { error = "Patient profile not found." });

                var app = await _appointmentService.BookAppointmentAsync(patientId.Value, appointment);
                return StatusCode(StatusCodes.Status201Created, new { message = "Appointment created successfully.", data = app });
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
                var patientId = await GetCurrentPatientIdAsync();
                if (patientId is null)
                    return NotFound(new { error = "Patient profile not found." });

                var result = await _appointmentService.RescheduleAppointment(id, availabilityId, patientId.Value);
                if (!result)
                    return NotFound(new { error = "Appointment not found or could not be rescheduled." });
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
            catch (UnauthorizedAccessException ua)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = ua.Message });
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
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            try
            {
                int? patientId = null;
                if (User.IsInRole("Patient"))
                {
                    patientId = await GetCurrentPatientIdAsync();
                    if (patientId is null)
                        return NotFound(new { error = "Patient profile not found." });
                }

                var result = await _appointmentService.UpdateAppointmentStatusAsync(id, Domain.Enums.AppointmentStatus.Cancelled, patientId);
                if (!result)
                    return NotFound(new { error = "Appointment not found." });
                return Ok(new { message = "Appointment deleted successfully." });
            }
            catch (UnauthorizedAccessException ua)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = ua.Message });

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost("review")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateReview([FromBody] CreateRatingDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var patientId = await GetCurrentPatientIdAsync();
            if (patientId is null)
                return NotFound(new { error = "Patient profile not found." });

            try
            {
                await _ratingService.CreateRating(patientId.Value, dto);
                return StatusCode(StatusCodes.Status201Created, new { message = "Review created successfully." });
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

        [HttpPut("review/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateRatingDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var patientId = await GetCurrentPatientIdAsync();
            if (patientId is null)
                return NotFound(new { error = "Patient profile not found." });


            try
            {
                var result = await _ratingService.UpdateRating(id, patientId.Value, dto);
                if (!result)
                    return NotFound(new { error = "Review not found." });

                return Ok(new { message = "Review updated successfully." });
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
                return StatusCode(StatusCodes.Status403Forbidden, new { error = ua.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpDelete("review/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var patientId = await GetCurrentPatientIdAsync();
                if (patientId is null)
                    return NotFound(new { error = "Patient profile not found." });

                var result = await _ratingService.DeleteRating(id, patientId.Value);
                if (!result)
                    return NotFound(new { error = "Review not found." });

                return Ok(new { message = "Review deleted successfully." });
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
                return StatusCode(StatusCodes.Status403Forbidden, new { error = ua.Message });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

    }
}
