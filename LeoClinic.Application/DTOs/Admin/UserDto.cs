
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Admin
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime DateJoined { get; set; }
    }
}
