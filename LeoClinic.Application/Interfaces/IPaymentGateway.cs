using LeoClinic.Application.DTOs.Payment;

namespace LeoClinic.Application.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentIntentResponse> CreatePaymentIntentAsync(PaymentIntentRequest request);
        Task<bool> ConfirmPaymentAsync(string paymentIntentId, string paymentMethodId);
        Task<bool> RefundAsync(string paymentIntentId);
        Task<WebhookEventResult> ParseWebhookEventAsync(string payload, string signatureHeader);
    }
}
