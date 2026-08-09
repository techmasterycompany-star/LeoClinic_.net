using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class PatientRegisterDto
    {
        [Required]
        public string ContactNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
    }
}