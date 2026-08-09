using Clinic.Models;

namespace Clinic.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);

        Task<Appointment?> GetAppointmentByIdAsync(int id);

        Task UpdateAppointmentAsync(Appointment appointment);

        Task<bool> PatientExistsAsync(int patientId);

        Task<bool> DoctorExistsAsync(int doctorId);

        Task<Availability?> GetAvailabilityAsync(int availabilityId);

        Task UpdateAvailabilityAsync(Availability availability);

        Task<Appointment?> GetAppointmentWithAvailabilityAsync(int appointmentId);
    }
}
