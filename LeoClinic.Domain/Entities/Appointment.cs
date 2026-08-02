using LeoClinic.Domain.Common;
using LeoClinic.Domain.Enums;


namespace LeoClinic.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string Notes { get; set; } = string.Empty;


        public int PatientId { get; set; }
        public PatientProfile PatientProfile { get; set; } = null!;
        public int DoctorId { get; set; }
        public DoctorProfile DoctorProfile { get; set; } = null!;
        public int AvailabilityId { get; set; }
        public Availability Availability { get; set; } = null!;


        public Payment? Payment { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
