using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Notification
{
    public class CreateNotificationDto
    {
        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.InApp;
    }
}
