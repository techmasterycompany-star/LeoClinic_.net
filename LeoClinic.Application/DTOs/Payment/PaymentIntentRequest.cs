namespace LeoClinic.Application.DTOs.Payment
{
    public class PaymentIntentRequest
    {
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
    }
}
