using Stripe.Checkout;

namespace Clinic.Services
{
    public class StripeService : IStripeService
    {
        public async Task<string> CreateCheckoutSessionAsync(
            decimal amount,
            string paymentMethod,
            int appointmentId,
            int patientId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },

                Mode = "payment",

                SuccessUrl = "https://localhost:7087/api/payment/success",

                CancelUrl = "https://localhost:7087/api/payment/cancel",

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,

                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",

                            UnitAmount = (long)(amount * 100),

                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Doctor Appointment"
                            }
                        }
                    }
                },

                Metadata = new Dictionary<string, string>
                {
                    { "AppointmentId", appointmentId.ToString() },
                    { "PatientId", patientId.ToString() },
                    { "PaymentMethod", paymentMethod }
                }
            };

            var service = new SessionService();

            Session session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}