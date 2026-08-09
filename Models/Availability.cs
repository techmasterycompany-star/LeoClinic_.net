namespace Clinic.Models
{
    public class Availability
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int LocationId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsBooked { get; set; }

        public DoctorProfile Doctor { get; set; }

        public Location Location { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
