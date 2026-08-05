namespace LeoClinic.Application.DTOs.Doctor
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
