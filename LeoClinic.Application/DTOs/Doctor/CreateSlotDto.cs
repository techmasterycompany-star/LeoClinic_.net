namespace LeoClinic.Application.DTOs.Doctor
{
    public class CreateSlotDto
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int LocationId { get; set; }
    }
}
