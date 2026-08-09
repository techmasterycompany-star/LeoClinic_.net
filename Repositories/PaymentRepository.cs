using Clinic.Data;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<List<Payment>> GetPaymentHistoryAsync(int patientId)
        {
            return await _context.Payments
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<bool> PatientExistsAsync(int patientId)
        {
            return await _context.PatientProfiles
                .AnyAsync(p => p.Id == patientId);
        }

        public async Task<bool> AppointmentExistsAsync(int appointmentId)
        {
            return await _context.Appointments
                .AnyAsync(a => a.Id == appointmentId);
        }
    }
}
