using System.ComponentModel.DataAnnotations.Schema;
namespace Clinic.Models
{
    public class DoctorProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Price { get; set; }

        public int SpecialtyId { get; set; }

        public string Bio { get; set; }

        public string ContactNumber { get; set; }

        public bool IsApproved { get; set; }

        public User User { get; set; }

        public Specialty Specialty { get; set; }

        public ICollection<DoctorLocation> DoctorLocations { get; set; } = new List<DoctorLocation>();

        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
