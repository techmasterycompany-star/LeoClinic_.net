namespace Clinic.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public VerificationCodeType Type { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? UsedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
