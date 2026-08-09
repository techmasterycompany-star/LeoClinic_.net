namespace Clinic.Models
{
    public class DoctorLocation
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int LocationId { get; set; }

        public DoctorProfile Doctor { get; set; }

        public Location Location { get; set; }
    }
}
