using LeoClinic.Domain.Common;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        public DoctorProfile? DoctorProfile { get; set; }
        public PatientProfile? PatientProfile { get; set; }
        public ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
