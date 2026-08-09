using LeoClinic.Application.DTOs.Notification;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> SendAsync(CreateNotificationDto dto);
        Task<NotificationDto?> GetByIdAsync(int id);
        Task<IEnumerable<NotificationDto>> GetByUserAsync(int userId);
        Task<int> GetUnreadCountAsync(int userId);
        Task<bool> MarkAsReadAsync(int id);
        Task<int> MarkAllAsReadAsync(int userId);
        Task<int> RetryFailedAsync();
        Task<IEnumerable<NotificationDto>> NotifyAppointmentAsync(int appointmentId, string message, NotificationType type);
    }
}
