namespace Clinic.DTOs
{
    public class PaymentHistoryDto
    {
        public int PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
