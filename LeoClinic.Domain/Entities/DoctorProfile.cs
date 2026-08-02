using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class DoctorProfile : BaseEntity
    {
        public decimal Price { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public bool IsApproved { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; } = null!;

        public ICollection<DoctorLocation> DoctorLocations { get; set; } = new List<DoctorLocation>();
        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();


    }
}
