using LeoClinic.Application.DTOs.Payment;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly INotificationRepository _notificationRepo;

        public PaymentService(IPaymentRepository paymentRepo, IAppointmentRepository appointmentRepo, INotificationRepository notificationRepo)
        {
            _paymentRepo = paymentRepo;
            _appointmentRepo = appointmentRepo;
            _notificationRepo = notificationRepo;
        }

        public async Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto)
        {
            if (dto.Amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(dto.AppointmentId);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            var existing = await _paymentRepo.GetByAppointmentAsync(dto.AppointmentId);
            if (existing != null)
            {
                throw new InvalidOperationException("This appointment already has a payment.");
            }

            var payment = new Payment
            {
                AppointmentId = dto.AppointmentId,
                PatientId = appointment.PatientId,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                Status = PaymentStatus.Completed,
                TransactionReference = GenerateTransactionReference(appointment.Id),
                PaymentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _paymentRepo.CreateAsync(payment);

            var patientUserId = appointment.PatientProfile?.UserId;
            if (patientUserId != null)
            {
                await _notificationRepo.CreateAsync(new Notification
                {
                    UserId = patientUserId.Value,
                    AppointmentId = appointment.Id,
                    Message = $"Your payment of {created.Amount} has been received successfully. Reference: {created.TransactionReference}",
                    Type = NotificationType.InApp,
                    Status = NotificationStatus.Sent,
                    SentAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return ToDto(created);
        }

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            if (payment == null)
            {
                return null;
            }
            return ToDto(payment);
        }

        public async Task<PaymentDto?> GetByAppointmentAsync(int appointmentId)
        {
            var payment = await _paymentRepo.GetByAppointmentAsync(appointmentId);
            if (payment == null)
            {
                return null;
            }
            return ToDto(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetByPatientAsync(int patientId)
        {
            var payments = await _paymentRepo.GetByPatientAsync(patientId);
            return payments.Select(ToDto);
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments = await _paymentRepo.GetAllAsync();
            return payments.Select(ToDto);
        }

        public async Task<PaymentDto?> RefundAsync(int id)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            if (payment == null)
            {
                return null;
            }

            if (payment.Status == PaymentStatus.Refunded)
            {
                throw new InvalidOperationException("Payment is already refunded.");
            }
            if (payment.Status != PaymentStatus.Completed)
            {
                throw new InvalidOperationException("Only completed payments can be refunded.");
            }

            payment.Status = PaymentStatus.Refunded;
            payment.UpdatedAt = DateTime.UtcNow;
            await _paymentRepo.UpdateAsync(payment);
            return ToDto(payment);
        }

        private static string GenerateTransactionReference(int appointmentId)
        {
            return $"PAY-{appointmentId}-{Guid.NewGuid():N}".ToUpperInvariant();
        }

        private static PaymentDto ToDto(Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                AppointmentId = payment.AppointmentId,
                PatientId = payment.PatientId,
                PatientName = payment.PatientProfile?.User != null
                    ? $"{payment.PatientProfile.User.FirstName} {payment.PatientProfile.User.LastName}"
                    : string.Empty,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                TransactionReference = payment.TransactionReference,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount
            };
        }
    }
}
