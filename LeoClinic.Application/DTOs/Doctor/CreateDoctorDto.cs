namespace LeoClinic.Application.DTOs.Doctor
{
    public class CreateDoctorDto
    {
        public decimal Price { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public int SpecialityId { get; set; }
        public List<int> LocationIds { get; set; } = new();
        public int UserId { get; set; }
    }
}
