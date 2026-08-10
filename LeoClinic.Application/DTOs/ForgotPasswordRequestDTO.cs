using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class ForgotPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
