using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.DTOs.Patient
{
    public class CreateAppointmentDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Zero and negative numbers are invalid for id")]
        public int DoctorId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Zero and negative numbers are invalid for id")]
        public int AvailabilityId { get; set; }
        public string? Notes { get; set; } = string.Empty;
    }
}
