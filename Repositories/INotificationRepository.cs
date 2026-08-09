using Clinic.Models;
namespace Clinic.Repositories
{
    public interface INotificationRepository
    {
        Task CreateNotificationAsync(Notification notification);

        Task<List<Notification>> GetUserNotificationsAsync(int userId);
    }
}
