using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllByPatientAsync(int patientId);
        Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
        Task<AppointmentDto> BookAppointmentAsync(CreateAppointmentDTO appointment);
        Task<bool> UpdateAppointmentStatusAsync(int id, AppointmentStatus status);
        Task<bool> RescheduleAppointment(int id, int availabilityId);
    }
}
