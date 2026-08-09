using Clinic.Models;

namespace Clinic.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);

        Task<List<Payment>> GetPaymentHistoryAsync(int patientId);

        Task<bool> PatientExistsAsync(int patientId);

        Task<bool> AppointmentExistsAsync(int appointmentId);
    }
}