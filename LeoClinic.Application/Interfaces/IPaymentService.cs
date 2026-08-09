using LeoClinic.Application.DTOs.Payment;

namespace LeoClinic.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentIntentDto> CreatePaymentIntentAsync(CreatePaymentIntentDto dto);
        Task<PaymentDto> ConfirmPaymentAsync(int paymentId, string paymentMethodId);
        Task<PaymentDto?> GetByIdAsync(int id);
        Task<PaymentDto?> GetByAppointmentAsync(int appointmentId);
        Task<IEnumerable<PaymentDto>> GetByPatientAsync(int patientId);
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto?> RefundAsync(int id);
        Task HandleWebhookAsync(string payload, string signatureHeader);
    }
}
