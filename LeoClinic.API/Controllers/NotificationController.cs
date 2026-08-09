using LeoClinic.Application.DTOs.Notification;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateNotificationDto dto)
        {
            try
            {
                var notification = await _notificationService.SendAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("appointment/{appointmentId}")]
        public async Task<IActionResult> NotifyAppointment(int appointmentId, [FromQuery] string message, [FromQuery] NotificationType type = NotificationType.InApp)
        {
            try
            {
                var notifications = await _notificationService.NotifyAppointmentAsync(appointmentId, message, type);
                return Ok(notifications);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var notifications = await _notificationService.GetByUserAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(int userId)
        {
            var count = await _notificationService.GetUnreadCountAsync(userId);
            return Ok(new { count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification is null)
                return NotFound();

            return Ok(notification);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkAsReadAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("user/{userId}/read-all")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            var count = await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { updated = count });
        }

        [HttpPost("retry-failed")]
        public async Task<IActionResult> RetryFailed()
        {
            var count = await _notificationService.RetryFailedAsync();
            return Ok(new { retried = count });
        }
    }
}
