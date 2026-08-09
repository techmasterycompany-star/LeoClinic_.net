using System;
namespace Clinic.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ContactNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public bool IsApproved { get; set; }

        public User User { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
