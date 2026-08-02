using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;


        public ICollection<DoctorLocation> DoctorLocations { get; set; } = new List<DoctorLocation>();
        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}
