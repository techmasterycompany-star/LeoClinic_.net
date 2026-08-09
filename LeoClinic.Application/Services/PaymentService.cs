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
        private readonly IPaymentGateway _paymentGateway;

        public PaymentService(IPaymentRepository paymentRepo, IAppointmentRepository appointmentRepo, INotificationRepository notificationRepo, IPaymentGateway paymentGateway)
        {
            _paymentRepo = paymentRepo;
            _appointmentRepo = appointmentRepo;
            _notificationRepo = notificationRepo;
            _paymentGateway = paymentGateway;
        }

        public async Task<PaymentIntentDto> CreatePaymentIntentAsync(CreatePaymentIntentDto dto)
        {
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

            var amount = dto.Amount ?? appointment.DoctorProfile?.Price ?? 0;
            if (amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            var intent = await _paymentGateway.CreatePaymentIntentAsync(new PaymentIntentRequest
            {
                AppointmentId = appointment.Id,
                Amount = amount,
                Currency = dto.Currency
            });

            var payment = new Payment
            {
                AppointmentId = appointment.Id,
                PatientId = appointment.PatientId,
                PaymentMethod = "Stripe",
                Amount = amount,
                Status = PaymentStatus.Pending,
                TransactionReference = intent.PaymentIntentId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _paymentRepo.CreateAsync(payment);

            return new PaymentIntentDto
            {
                PaymentId = created.Id,
                PaymentIntentId = created.TransactionReference,
                ClientSecret = intent.ClientSecret,
                Amount = amount,
                Currency = intent.Currency,
                Status = created.Status
            };
        }

        public async Task<PaymentDto> ConfirmPaymentAsync(int paymentId, string paymentMethodId)
        {
            if (string.IsNullOrWhiteSpace(paymentMethodId))
            {
                throw new ArgumentException("Payment method is required.");
            }

            var payment = await _paymentRepo.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found.");
            }

            if (payment.Status == PaymentStatus.Completed)
            {
                return ToDto(payment);
            }

            if (payment.Status != PaymentStatus.Pending)
            {
                throw new InvalidOperationException($"Payment cannot be confirmed while in status {payment.Status}.");
            }

            var succeeded = await _paymentGateway.ConfirmPaymentAsync(payment.TransactionReference, paymentMethodId);
            if (!succeeded)
            {
                payment.Status = PaymentStatus.Failed;
                payment.UpdatedAt = DateTime.UtcNow;
                await _paymentRepo.UpdateAsync(payment);
                throw new InvalidOperationException("Payment was not successful.");
            }

            return await CompletePaymentAsync(payment);
        }

        public async Task HandleWebhookAsync(string payload, string signatureHeader)
        {
            var evt = await _paymentGateway.ParseWebhookEventAsync(payload, signatureHeader);

            if (evt.Type == "payment_intent.succeeded")
            {
                var payment = await _paymentRepo.GetByTransactionReferenceAsync(evt.PaymentIntentId);
                if (payment != null && payment.Status != PaymentStatus.Completed)
                {
                    await CompletePaymentAsync(payment);
                }
            }
            else if (evt.Type == "payment_intent.payment_failed")
            {
                var payment = await _paymentRepo.GetByTransactionReferenceAsync(evt.PaymentIntentId);
                if (payment != null && payment.Status == PaymentStatus.Pending)
                {
                    payment.Status = PaymentStatus.Failed;
                    payment.UpdatedAt = DateTime.UtcNow;
                    await _paymentRepo.UpdateAsync(payment);
                }
            }
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

            var refunded = await _paymentGateway.RefundAsync(payment.TransactionReference);
            if (!refunded)
            {
                throw new InvalidOperationException("Refund could not be processed.");
            }

            payment.Status = PaymentStatus.Refunded;
            payment.UpdatedAt = DateTime.UtcNow;
            await _paymentRepo.UpdateAsync(payment);
            return ToDto(payment);
        }

        private async Task<PaymentDto> CompletePaymentAsync(Payment payment)
        {
            payment.Status = PaymentStatus.Completed;
            payment.PaymentDate = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;
            await _paymentRepo.UpdateAsync(payment);

            var patientUserId = payment.PatientProfile?.UserId;
            if (patientUserId != null)
            {
                await _notificationRepo.CreateAsync(new Notification
                {
                    UserId = patientUserId.Value,
                    AppointmentId = payment.AppointmentId,
                    Message = $"Your payment of {payment.Amount} has been received successfully. Reference: {payment.TransactionReference}",
                    Type = NotificationType.InApp,
                    Status = NotificationStatus.Sent,
                    SentAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return ToDto(payment);
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
