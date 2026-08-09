using Clinic.DTOs;
using Clinic.Exceptions;
using Clinic.Models;
using Clinic.Repositories;

namespace Clinic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> MakePaymentAsync(MakePaymentDto dto)
        {
            if (!await _paymentRepository.PatientExistsAsync(dto.PatientId))
                throw new NotFoundException("Patient not found.");

            if (!await _paymentRepository.AppointmentExistsAsync(dto.AppointmentId))
                throw new NotFoundException("Appointment not found.");

            var payment = new Payment
            {
                PatientId = dto.PatientId,
                AppointmentId = dto.AppointmentId,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = DateTime.Now,
                Amount = dto.Amount
            };

            return await _paymentRepository.CreatePaymentAsync(payment);
        }

        public async Task<List<PaymentHistoryDto>> GetPaymentHistoryAsync(int patientId)
        {
            var payments = await _paymentRepository.GetPaymentHistoryAsync(patientId);

            return payments.Select(p => new PaymentHistoryDto
            {
                PaymentId = p.Id,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaymentDate = p.PaymentDate
            }).ToList();
        }
    }
}
