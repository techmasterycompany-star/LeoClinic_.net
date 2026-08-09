namespace LeoClinic.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int AppointmentId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
