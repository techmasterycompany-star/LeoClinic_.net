namespace LeoClinic.Application.DTOs.Admin
{
    public class DoctorLocationDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;
    }
}
