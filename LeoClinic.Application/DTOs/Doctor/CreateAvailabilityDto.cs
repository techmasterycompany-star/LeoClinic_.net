namespace LeoClinic.Application.DTOs.Doctor
{
    public class CreateAvailabilityDto
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int LocationId { get; set; }
        public int DoctorId { get; set; }
    }
}
