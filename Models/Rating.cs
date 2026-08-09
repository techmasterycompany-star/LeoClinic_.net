namespace Clinic.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public byte Rate { get; set; }

        public string Review { get; set; }

        public DateTime CreatedAt { get; set; }

        public DoctorProfile Doctor { get; set; }

        public PatientProfile Patient { get; set; }
    }
}
