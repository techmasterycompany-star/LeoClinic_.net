using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class PatientProfile : BaseEntity
    {
        public string ContactNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsApproved { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
