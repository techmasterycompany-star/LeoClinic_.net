using LeoClinic.Domain.Common;

namespace LeoClinic.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
