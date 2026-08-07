using LeoClinic.Domain.Entities;

namespace LeoClinic.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        //Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        // 
        Task<IEnumerable<Appointment>> GetAllByPatientAsync(int patientId);
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<Appointment> BookAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> isAppointmentCompleted(int id);
    }
}
