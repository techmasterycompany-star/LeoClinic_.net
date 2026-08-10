using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class DoctorRegisterDto
    {
        [Range(100, 10000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(300)]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Contact number must be an 11-digit phone number starting with 01 (e.g. 01229494438).")]
        public string ContactNumber { get; set; } = string.Empty;

        public int SpecialityId { get; set; }
    }
}