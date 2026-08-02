using LeoClinic.Domain.Common;
using LeoClinic.Domain.Enums;


namespace LeoClinic.Domain.Entities
{
    public class VerificationCode : BaseEntity
    {
        public VerificationType Type { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsedAt { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
