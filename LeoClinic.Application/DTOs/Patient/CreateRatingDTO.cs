using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs.Patient
{
    public class CreateRatingDTO
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating Value must be between 1 and 5.")]
        public int RatingValue { get; set; }

        [StringLength(1000, ErrorMessage = "Review cannot exceed 1000 characters.")]
        public string? Review { get; set; }
    }
}