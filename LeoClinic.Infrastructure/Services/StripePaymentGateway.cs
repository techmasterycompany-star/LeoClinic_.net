using LeoClinic.Application.DTOs.Payment;
using LeoClinic.Application.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;

namespace LeoClinic.Infrastructure.Services
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly StripeSettings _settings;

        public StripePaymentGateway(IOptions<StripeSettings> settings)
        {
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.SecretKey;
        }

        public async Task<PaymentIntentResponse> CreatePaymentIntentAsync(PaymentIntentRequest request)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)Math.Round(request.Amount * 100),
                    Currency = request.Currency,
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions { Enabled = true },
                    Metadata = new Dictionary<string, string> { ["appointment_id"] = request.AppointmentId.ToString() }
                };

                var service = new PaymentIntentService();
                var intent = await service.CreateAsync(options);

                return new PaymentIntentResponse
                {
                    PaymentIntentId = intent.Id,
                    ClientSecret = intent.ClientSecret,
                    Amount = request.Amount,
                    Currency = intent.Currency
                };
            }
            catch (StripeException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public async Task<bool> ConfirmPaymentAsync(string paymentIntentId, string paymentMethodId)
        {
            try
            {
                var options = new PaymentIntentConfirmOptions { PaymentMethod = paymentMethodId };
                var service = new PaymentIntentService();
                var intent = await service.ConfirmAsync(paymentIntentId, options);
                return intent.Status == "succeeded";
            }
            catch (StripeException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public async Task<bool> RefundAsync(string paymentIntentId)
        {
            try
            {
                var service = new RefundService();
                await service.CreateAsync(new RefundCreateOptions { PaymentIntent = paymentIntentId });
                return true;
            }
            catch (StripeException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public Task<WebhookEventResult> ParseWebhookEventAsync(string payload, string signatureHeader)
        {
            try
            {
                var evt = EventUtility.ConstructEvent(payload, signatureHeader, _settings.WebhookSecret);
                var paymentIntent = evt.Data.Object as PaymentIntent;

                return Task.FromResult(new WebhookEventResult
                {
                    Type = evt.Type,
                    PaymentIntentId = paymentIntent?.Id ?? string.Empty
                });
            }
            catch (StripeException ex)
            {
                throw new InvalidOperationException("Invalid Stripe webhook signature.", ex);
            }
        }
    }
}
