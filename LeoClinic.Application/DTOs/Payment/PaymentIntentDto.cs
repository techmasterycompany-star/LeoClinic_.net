using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Payment
{
    public class PaymentIntentDto
    {
        public int PaymentId { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
    }
}
