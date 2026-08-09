using LeoClinic.Domain.Entities;

namespace LeoClinic.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment?> GetByAppointmentAsync(int appointmentId);
        Task<Payment?> GetByTransactionReferenceAsync(string transactionReference);
        Task<IEnumerable<Payment>> GetByPatientAsync(int patientId);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task UpdateAsync(Payment payment);
    }
}
