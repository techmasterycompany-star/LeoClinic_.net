namespace Clinic.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AppointmentId { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public User User { get; set; }

        public Appointment Appointment { get; set; }
    }
}
