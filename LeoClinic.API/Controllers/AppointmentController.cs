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
            var app = await _appointmentService.BookAppointmentAsync(appointment);
            return Ok(app);
        }

        [HttpPost("{id}/reschedule/{availabilityId}")]
        public async Task<IActionResult> RescheduleAppointment(int id, int availabilityId)
        {
            var result = await _appointmentService.RescheduleAppointment(id, availabilityId);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _appointmentService.UpdateAppointmentStatusAsync(id, Domain.Enums.AppointmentStatus.Cancelled);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
