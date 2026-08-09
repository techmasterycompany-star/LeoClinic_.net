namespace Clinic.DTOs
{
    public class BookAppointmentDto
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int AvailabilityId { get; set; }

        public string? Notes { get; set; }
    }
}
