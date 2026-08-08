namespace LeoClinic.Application.DTOs.Admin
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public int PatientId { get; set; }

        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }
    }
}
