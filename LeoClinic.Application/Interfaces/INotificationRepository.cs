using LeoClinic.Domain.Entities;

namespace LeoClinic.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<Notification?> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetByUserAsync(int userId);
        Task<IEnumerable<Notification>> GetUnreadByUserAsync(int userId);
        Task<int> GetUnreadCountAsync(int userId);
        Task<IEnumerable<Notification>> GetFailedAsync();
        Task UpdateAsync(Notification notification);
    }
}
