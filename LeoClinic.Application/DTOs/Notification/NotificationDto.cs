using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public int RetryCount { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime SentAt { get; set; }
        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
