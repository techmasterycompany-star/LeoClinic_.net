using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class ResetPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
