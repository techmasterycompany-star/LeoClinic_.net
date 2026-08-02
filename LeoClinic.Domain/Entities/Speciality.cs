using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public ICollection<DoctorProfile> DoctorProfiles { get; set; } = new List<DoctorProfile>();
    }
}
