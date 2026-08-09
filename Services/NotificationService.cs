using Clinic.Models;
using Clinic.Repositories;

namespace Clinic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SendNotificationAsync(int userId, int appointmentId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                AppointmentId = appointmentId,
                Message = message,
                SentAt = DateTime.Now
            };

            await _notificationRepository.CreateNotificationAsync(notification);
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _notificationRepository.GetUserNotificationsAsync(userId);
        }
    }
}
