using Clinic.DTOs;
using Clinic.Models;

namespace Clinic.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> BookAppointmentAsync(BookAppointmentDto dto);

        Task CancelAppointmentAsync(int appointmentId);
    }

}
