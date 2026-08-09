using System;
namespace Clinic.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime DateJoined { get; set; }

        public PatientProfile PatientProfile { get; set; }

        public DoctorProfile DoctorProfile { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
    }
}
