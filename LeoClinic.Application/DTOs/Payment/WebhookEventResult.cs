namespace LeoClinic.Application.DTOs.Payment
{
    public class WebhookEventResult
    {
        public string Type { get; set; } = string.Empty;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
