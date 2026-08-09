using Clinic.Models;
namespace Clinic.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, int appointmentId, string message);

        Task<List<Notification>> GetUserNotificationsAsync(int userId);
    }
}
