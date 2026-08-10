using System;
using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class PatientRegisterDto
    {
        [Required]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Contact number must be an 11-digit phone number starting with 01 (e.g. 01229494438).")]
        public string ContactNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
    }
}