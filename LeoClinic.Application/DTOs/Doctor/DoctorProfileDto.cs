namespace LeoClinic.Application.DTOs.Doctor
{
    public class DoctorProfileDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; } = string.Empty;
        public List<LocationDto> Locations { get; set; } = new();
        public double AverageRating { get; set; }
    }
}
