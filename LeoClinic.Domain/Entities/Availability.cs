using LeoClinic.Domain.Common;

namespace LeoClinic.Domain.Entities
{
    public class Availability : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }


        public int DoctorId { get; set; }
        public DoctorProfile DoctorProfile { get; set; } = null!;
        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;


        public Appointment? Appointment { get; set; }
    }
}
