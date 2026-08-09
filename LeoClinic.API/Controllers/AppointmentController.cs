using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDTO appointment)
        {
            if (appointment is null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var app = await _appointmentService.BookAppointmentAsync(appointment);
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

        [HttpPatch("{id}/reschedule/{availabilityId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> RescheduleAppointment(int id, int availabilityId)
        {
            try
            {
                var result = await _appointmentService.RescheduleAppointment(id, availabilityId);
                if (!result)
                    return NotFound();
                return NoContent();
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

        [HttpPatch("{id}/cancel")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                var result = await _appointmentService.UpdateAppointmentStatusAsync(id, Domain.Enums.AppointmentStatus.Cancelled);
                if (!result)
                    return NotFound();
                return NoContent();
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
    }
}
