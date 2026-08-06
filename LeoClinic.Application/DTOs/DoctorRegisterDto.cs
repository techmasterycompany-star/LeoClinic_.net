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
        [Phone]
        public string ContactNumber { get; set; } = string.Empty;
        public int SpecialityId { get; set; }
    }
}