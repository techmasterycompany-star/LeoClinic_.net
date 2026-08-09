namespace Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int AvailabilityId { get; set; }

        public AppointmentStatus Status { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public PatientProfile Patient { get; set; }

        public DoctorProfile Doctor { get; set; }

        public Availability Availability { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
