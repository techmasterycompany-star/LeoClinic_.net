using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientProfile(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient is null) return NotFound();

            return Ok(patient);
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedPatients()
        {
            var patients = await _patientService.GetApprovedPatientsAsync();
            return Ok(patients);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO dto)
        {
            if (dto is null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var patient = await _patientService.CreatePatientProfileAsync(dto);
                return CreatedAtAction(nameof(GetPatientProfile), new { id = patient.Id }, patient);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var result = await _patientService.DeletePatientAsync(id);
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

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovePatient(int id)
        {
            try
            {
                var result = await _patientService.ApprovePatientAsync(id);
                if (!result)
                    return NotFound();

                return Ok(new { message = "Patient approved successfully" });
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

        [HttpGet("{id}/appointments")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAppointmentsByPatient(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetAllByPatientAsync(id);
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

            try
            {
                var result = await _ratingService.CreateRating(dto);
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

            try
            {
                var result = await _ratingService.UpdateRating(id, dto);
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

        [HttpDelete("/review/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var result = await _ratingService.DeleteRating(id);
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
    }
}
