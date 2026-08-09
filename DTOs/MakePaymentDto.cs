namespace Clinic.DTOs
{
    public class MakePaymentDto
    {
        public int PatientId { get; set; }

        public int AppointmentId { get; set; }

        public string PaymentMethod { get; set; }

        public decimal Amount { get; set; }
    }
}
