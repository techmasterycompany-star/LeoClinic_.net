namespace LeoClinic.Application.DTOs.Admin
{
    public class RatingDto
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public string Review { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        public int PatientId { get; set; }
    }
}
