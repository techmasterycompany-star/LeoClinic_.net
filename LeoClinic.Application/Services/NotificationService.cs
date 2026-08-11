using LeoClinic.Application.DTOs.Notification;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        public NotificationService(INotificationRepository notificationRepo, IAppointmentRepository appointmentRepo)
        {
            _notificationRepo = notificationRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<NotificationDto> SendAsync(CreateNotificationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
            {
                throw new ArgumentException("Notification message is required.");
            }

            if (dto.UserId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            if (!Enum.IsDefined(typeof(NotificationType), dto.Type))
            {
                throw new ArgumentException($"Invalid notification type: {dto.Type}");
            }

            if (dto.AppointmentId.HasValue && dto.AppointmentId.Value > 0)
            {
                var appointment = await _appointmentRepo.GetAppointmentByIdAsync(dto.AppointmentId.Value);
                if (appointment == null)
                {
                    throw new KeyNotFoundException($"Appointment with ID {dto.AppointmentId} not found.");
                }

                if (appointment.Status == AppointmentStatus.Cancelled)
                {
                    throw new InvalidOperationException("Cannot send notification for a cancelled appointment.");
                }

                if (appointment.Status == AppointmentStatus.Rejected)
                {
                    throw new InvalidOperationException("Cannot send notification for a rejected appointment.");
                }
            }

            var notification = new Notification
            {
                UserId = dto.UserId,
                AppointmentId = dto.AppointmentId,
                Message = dto.Message,
                Type = dto.Type,
                Status = NotificationStatus.Sent,
                RetryCount = 0,
                IsRead = false,
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _notificationRepo.CreateAsync(notification);
            return ToDto(created);
        }

        public async Task<IEnumerable<NotificationDto>> NotifyAppointmentAsync(int appointmentId, string message, NotificationType type)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot send notification for a cancelled appointment.");
            }

            if (appointment.Status == AppointmentStatus.Rejected)
            {
                throw new InvalidOperationException("Cannot send notification for a rejected appointment.");
            }

            var userIds = new List<int>();
            if (appointment.PatientProfile?.UserId != null)
                userIds.Add(appointment.PatientProfile.UserId);
            if (appointment.DoctorProfile?.UserId != null)
                userIds.Add(appointment.DoctorProfile.UserId);

            var result = new List<NotificationDto>();
            foreach (var userId in userIds)
            {
                var notification = await SendAsync(new CreateNotificationDto
                {
                    UserId = userId,
                    AppointmentId = appointmentId,
                    Message = message,
                    Type = type
                });
                result.Add(notification);
            }
            return result;
        }

        public async Task<NotificationDto?> GetByIdAsync(int id)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);
            if (notification == null)
            {
                return null;
            }
            return ToDto(notification);
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserAsync(int userId)
        {
            var notifications = await _notificationRepo.GetByUserAsync(userId);
            return notifications.Select(ToDto);
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _notificationRepo.GetUnreadCountAsync(userId);
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);
            if (notification == null)
            {
                return false;
            }

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                notification.UpdatedAt = DateTime.UtcNow;
                await _notificationRepo.UpdateAsync(notification);
            }
            return true;
        }

        public async Task<int> MarkAllAsReadAsync(int userId)
        {
            var unread = await _notificationRepo.GetUnreadByUserAsync(userId);
            foreach (var notification in unread)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                notification.UpdatedAt = DateTime.UtcNow;
                await _notificationRepo.UpdateAsync(notification);
            }
            return unread.Count();
        }

        public async Task<int> RetryFailedAsync()
        {
            var failed = await _notificationRepo.GetFailedAsync();
            int retried = 0;
            foreach (var notification in failed)
            {
                if (notification.RetryCount >= 3)
                {
                    continue;
                }

                notification.RetryCount++;
                notification.Status = NotificationStatus.Sent;
                notification.SentAt = DateTime.UtcNow;
                notification.UpdatedAt = DateTime.UtcNow;
                await _notificationRepo.UpdateAsync(notification);
                retried++;
            }
            return retried;
        }

        private static NotificationDto ToDto(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                Message = notification.Message,
                Type = notification.Type,
                Status = notification.Status,
                RetryCount = notification.RetryCount,
                IsRead = notification.IsRead,
                ReadAt = notification.ReadAt,
                SentAt = notification.SentAt,
                UserId = notification.UserId,
                AppointmentId = notification.AppointmentId
            };
        }
    }
}
