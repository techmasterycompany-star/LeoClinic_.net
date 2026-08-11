using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.DTOs.Doctor
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string Notes { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public decimal? PaymentAmount { get; set; }
    }
}
