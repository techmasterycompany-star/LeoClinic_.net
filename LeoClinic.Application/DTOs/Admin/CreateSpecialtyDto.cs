
using System.ComponentModel.DataAnnotations;


namespace LeoClinic.Application.DTOs.Admin
{
    public class CreateSpecialtyDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
