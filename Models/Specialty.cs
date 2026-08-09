namespace Clinic.Models
{
    public class Specialty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<DoctorProfile> DoctorProfiles { get; set; } = new List<DoctorProfile>();
    }
}
