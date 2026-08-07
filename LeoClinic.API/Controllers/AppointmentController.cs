using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
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
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDTO appointment)
        {
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
        }

        [HttpPost("{id}/reschedule/{availabilityId}")]
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
        }

        [HttpPatch("{id}/cancel")]
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
        }
    }
}
