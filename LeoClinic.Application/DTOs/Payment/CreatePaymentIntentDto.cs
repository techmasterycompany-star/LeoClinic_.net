namespace LeoClinic.Application.DTOs.Payment
{
    public class CreatePaymentIntentDto
    {
        public int AppointmentId { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; } = "usd";
    }
}
