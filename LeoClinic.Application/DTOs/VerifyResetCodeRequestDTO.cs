using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class VerifyResetCodeRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
