namespace Clinic.Services
{
    public interface IStripeService
    {
        Task<string> CreateCheckoutSessionAsync(
            decimal amount,
            string paymentMethod,
            int appointmentId,
            int patientId);
    }
}
