using LeoClinic.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }
        public DoctorRegisterDto? Doctor { get; set; }
        public PatientRegisterDto? Patient { get; set; }
    }
}
