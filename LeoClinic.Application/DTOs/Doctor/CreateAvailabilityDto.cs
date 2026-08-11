namespace LeoClinic.Application.DTOs.Doctor
{
    public class CreateAvailabilityDto
    {
        public DateTime Date { get; set; }
        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
        public int LocationId { get; set; }
        public int DoctorId { get; set; }
    }
}
