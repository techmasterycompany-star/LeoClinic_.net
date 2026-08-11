using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Application.Services;
using LeoClinic.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService service;
        public DoctorController(IDoctorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await service.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchDoctor(string? specialty, int? locationId, string? name)
        {
            var doctors = await service.SearchDoctorAsync(specialty, locationId, name, true);
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorProfile(int id)
        {
            var doctor = await service.GetDoctorProfileAsync(id);
            if (doctor is null) return NotFound();

            return Ok(doctor);
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedDoctors()
        {
            var doctors = await service.GetApprovedDoctorsAsync();
            return Ok(doctors);
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin,Doctor")]
        //public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        //{
        //    var doctor = await service.CreateProfileAsync(dto);
        //    return CreatedAtAction(nameof(GetDoctorProfile), new { id = doctor.Id }, doctor);
        //}

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto dto)
        {
            var doctor = await service.UpdateProfileAsync(id, dto);
            if (doctor is null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await service.DeleteProfileAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/slots")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> AddSlot(int id, [FromBody] CreateAvailabilityDto dto)
        {
            if (User.IsInRole("Doctor"))
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var doctor = await service.GetDoctorByUserIdAsync(userId);
                if (doctor is null || doctor.Id != id)
                    return Forbid();
            }

            dto.DoctorId = id;
            var slots = await service.CreateSlotAsync(dto);
            return Ok(slots);
        }

        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetSlots(int id)
        {
            var slots = await service.GetSlotsByDoctorIdAsync(id);
            return Ok(slots);
        }

        [HttpGet("{id}/appointments")]
        public async Task<IActionResult> GetAppointments(int id)
        {
            var appointments = await service.GetAppointmentsAsync(id);
            return Ok(appointments);
        }

        [HttpGet("{id}/appointments/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int id, int appointmentId)
        {
            var appointment = await service.GetAppointmentByIdAsync(appointmentId);
            if (appointment is null)
                return NotFound();

            if (appointment.DoctorId != id)
                return Forbid();

            return Ok(appointment);
        }

        [HttpPut("{id}/appointments/{appointmentId}/status")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAppointment(int id, int appointmentId, [FromQuery] AppointmentStatus status)
        {
            var appointment = await service.GetAppointmentByIdAsync(appointmentId);
            if (appointment is null) return NotFound();

            if (appointment.DoctorId != id) return Forbid();

            var result = await service.UpdateAppointmentAsync(appointmentId, status);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var reviews = await service.GetReviewsByDoctorIdAsync(id);
            return Ok(reviews);
        }

    }
}
