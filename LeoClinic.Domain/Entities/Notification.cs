using LeoClinic.Domain.Common;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.InApp;
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
        public int RetryCount { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
