using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeoClinic.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _dbContext;
        public PaymentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _dbContext.Payments
                .Include(p => p.PatientProfile).ThenInclude(pp => pp.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> GetByAppointmentAsync(int appointmentId)
        {
            return await _dbContext.Payments
                .Include(p => p.PatientProfile).ThenInclude(pp => pp.User)
                .FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
        }

        public async Task<IEnumerable<Payment>> GetByPatientAsync(int patientId)
        {
            return await _dbContext.Payments
                .Where(p => p.PatientId == patientId)
                .Include(p => p.PatientProfile).ThenInclude(pp => pp.User)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _dbContext.Payments
                .Include(p => p.PatientProfile).ThenInclude(pp => pp.User)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _dbContext.Payments.Update(payment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
