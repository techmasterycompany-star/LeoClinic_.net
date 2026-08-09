namespace Clinic.Models
{

    public class Payment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int AppointmentId { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public PatientProfile Patient { get; set; }

        public Appointment Appointment { get; set; }
    }
}
