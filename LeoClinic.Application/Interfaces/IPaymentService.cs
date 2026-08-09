using LeoClinic.Application.DTOs.Payment;

namespace LeoClinic.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto);
        Task<PaymentDto?> GetByIdAsync(int id);
        Task<PaymentDto?> GetByAppointmentAsync(int appointmentId);
        Task<IEnumerable<PaymentDto>> GetByPatientAsync(int patientId);
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto?> RefundAsync(int id);
    }
}
