using LeoClinic.Application.DTOs.Admin;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("Specialities")]
        public async Task<IActionResult> GetAllSpecialities()
        {
            var specialities = await _adminService.GetAllSpecialtiesAsync();
            return Ok(specialities);
        }

        [HttpGet("Specialities/{id}")]
        public async Task<IActionResult> GetSpecialityById(int id)
        {
            var speciality = await _adminService.GetSpecialtyByIdAsync(id);

            if (speciality == null)
                return NotFound();

            return Ok(speciality);
        }

        [HttpPost("Specialities")]
        public async Task<IActionResult> CreateSpeciality([FromBody] CreateSpecialtyDto dto)
        {
            await _adminService.CreateSpecialtyAsync(dto);

            return Ok();
        }

        [HttpPut("Specialities/{id}")]
        public async Task<IActionResult> UpdateSpecialty(int id, [FromBody] UpdateSpecialtyDto dto)
        {
            var updated = await _adminService.UpdateSpecialtyAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Specialities/{id}")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            var deleted = await _adminService.DeleteSpecialtyAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Doctors/Pending")]
        public async Task<IActionResult> GetPendingDoctors()
        {
            var pendingDoctors = await _adminService.GetPendingDoctorsAsync();
            return Ok(pendingDoctors);
        }

        [HttpPut("Doctors/{id}/Approve")]
        public async Task<IActionResult> ApproveDoctor(int id)
        {
            var approved = await _adminService.ApproveDoctorAsync(id);

            if (!approved)
                return NotFound("Doctor not found or already approved");

            return NoContent();
        }

        [HttpPut("Doctors/{id}/Reject")]
        public async Task<IActionResult> RejectDoctor(int id)
        {
            var rejected = await _adminService.RejectDoctorAsync(id);

            if (!rejected)
                return NotFound("Doctor not found or already rejected");

            return NoContent();
        }

        [HttpGet("Locations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _adminService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("Locations/{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _adminService.GetLocationByIdAsync(id);

            if (location == null)
                return NotFound();

            return Ok(location);
        }

        [HttpPost("Locations")]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto dto)
        {
            await _adminService.CreateLocationAsync(dto);

            return Ok();
            // Later, if the service returns the created object:
            // return CreatedAtAction(nameof(GetLocationById), new { id = created.Id }, created);
        }

        [HttpPut("Locations/{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateLocationDto dto)
        {
            var updated = await _adminService.UpdateLocationAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Locations/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var deleted = await _adminService.DeleteLocationAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Doctors/{doctorId}/Locations")]
        public async Task<IActionResult> GetDoctorLocations(int doctorId)
        {
            var locations = await _adminService.GetDoctorLocationsAsync(doctorId);

            return Ok(locations);
        }

        [HttpGet("Locations/{locationId}/Doctors")]
        public async Task<IActionResult> GetLocationDoctors(int locationId)
        {
            var doctors = await _adminService.GetLocationDoctorsAsync(locationId);

            return Ok(doctors);
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> SearchAppointments([FromQuery] AppointmentStatus? status, [FromQuery] int? doctorId, [FromQuery] int? patientId, [FromQuery] DateTime? date)
        {
            var appointments = await _adminService.GetAppointmentsAsync(status,doctorId,patientId,date);

            return Ok(appointments);
        }

        [HttpPut("appointments/{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var cancelld = await _adminService.CancelAppointmentAsync(id);

            if (!cancelld)
            {
                return NotFound("Appointment not found or is already cancelled.");
            }

            return Ok("Appointment cancelled successfully.");
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] string? email, [FromQuery] UserRole? role, [FromQuery] bool? isBlocked)
        {
            var users = await _adminService.GetUsersAsync(email, role, isBlocked);
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPut("users/{id}/block")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var blocked = await _adminService.BlockUserAsync(id);
            if (!blocked)
            {
                return NotFound("User not found or is already blocked.");
            }
            return Ok("User blocked successfully.");
        }

        [HttpPut("users/{id}/unblock")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var unblocked = await _adminService.UnblockUserAsync(id);
            if (!unblocked)
            {
                return NotFound("User not found or is not blocked.");
            }
            return Ok("User unblocked successfully.");
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard =
                await _adminService.GetDashboardStatisticsAsync();

            return Ok(dashboard);
        }

        [HttpGet("payments/revenue")]
        public async Task<IActionResult> GetRevenue()
        {
            var revenue = await _adminService.GetRevenueAsync();

            return Ok(revenue);
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetPayments([FromQuery] int? doctorId, [FromQuery] int? patientId, [FromQuery] DateTime? date)
        {
            var payments = await _adminService.GetPaymentsAsync(doctorId, patientId, date);
            return Ok(payments);
        }

        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _adminService.GetPaymentByIdAsync(id);

            if (payment == null)
            {
                return NotFound("Payment not found.");
            }

            return Ok(payment);
        }
      
        [HttpGet("reviews")]
        public async Task<IActionResult> GetRatings([FromQuery] int? doctorId,[FromQuery] int? patientId, [FromQuery] int? rate)
        {
            var ratings = await _adminService.GetRatingsAsync(doctorId,patientId, rate);

            return Ok(ratings);
        }

        [HttpGet("reviews/{id}")]
        public async Task<IActionResult> GetRatingById(int id)
        {
            var rating = await _adminService.GetRatingByIdAsync(id);

            if (rating == null)
            {
                return NotFound("Review not found.");
            }

            return Ok(rating);
        }

        [HttpDelete("reviews/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var result = await _adminService.DeleteRatingAsync(id);

            if (!result)
            {
                return NotFound("Review not found.");
            }

            return Ok("Review deleted successfully.");
        }
    }
}
