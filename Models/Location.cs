namespace Clinic.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public ICollection<DoctorLocation> DoctorLocations { get; set; } = new List<DoctorLocation>();

        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}
