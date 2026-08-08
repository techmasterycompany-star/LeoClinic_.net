using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Admin
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public AppointmentStatus Status { get; set; }

        public string Notes { get; set; } = string.Empty;

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int AvailabilityId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
