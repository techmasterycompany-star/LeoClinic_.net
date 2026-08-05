using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
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
            var patient = await _patientService.CreatePatientProfileAsync(dto);
            return CreatedAtAction(nameof(GetPatientProfile), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientProfileDTO dto)
        {
            var result = await _patientService.UpdatePatientProfileAsync(id, dto);
            if(!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovePatient(int id)
        {
            var result = await _patientService.ApprovePatientAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Patient approved successfully" });
        }
    }
}
