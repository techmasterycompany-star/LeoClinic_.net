using Clinic.DTOs;
using Clinic.Services;
using Microsoft.AspNetCore.Mvc;
using Clinic.Exceptions;
using Clinic.Exceptions;

namespace Clinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(BookAppointmentDto dto)
        {
            try
            {
                var appointment = await _appointmentService.BookAppointmentAsync(dto);

                return Ok(appointment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                await _appointmentService.CancelAppointmentAsync(id);

                return Ok(new
                {
                    Message = "Appointment cancelled successfully."
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
