namespace LeoClinic.Application.DTOs.Doctor
{
    public class AvailabilityDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
        public LocationDto Location { get; set; } = new();
    }
}
