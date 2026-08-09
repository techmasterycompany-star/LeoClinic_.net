using Clinic.DTOs;
using Clinic.Models;

namespace Clinic.Services
{
    public interface IPaymentService
    {
        Task<Payment> MakePaymentAsync(MakePaymentDto dto);

        Task<List<PaymentHistoryDto>> GetPaymentHistoryAsync(int patientId);
    }
}