
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Admin
{
    public class UserFilterDto
    {
        public string? Email { get; set; }

        public UserRole? Role { get; set; }

        public bool? IsBlocked { get; set; }
    }
}
