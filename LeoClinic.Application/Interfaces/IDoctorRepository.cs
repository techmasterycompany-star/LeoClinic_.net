using LeoClinic.Domain.Entities;

namespace LeoClinic.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<DoctorProfile>> SearchAsync(string? specialty, int? locationId, string? name, bool? isApproved);
        Task<IEnumerable<DoctorProfile>> GetAllAsync();
        Task<DoctorProfile?> GetByIdAsync(int id);
        Task<DoctorProfile> CreateAsync(DoctorProfile doctorProfile);
        void Update(DoctorProfile doctorProfile);
        void Delete(DoctorProfile doctorProfile);
        Task<IEnumerable<DoctorProfile>> GetApprovedDoctorsAsync();


        Task<Availability> AddSlotAsync(Availability slot);
        Task<IEnumerable<Availability>> GetSlotsByDoctorIdAsync(int doctorId);


        Task<IEnumerable<Appointment>> GetAppointmentsAsync(int doctorId);
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);


        Task<IEnumerable<Rating>> GetReviewsByDoctorIdAsync(int doctorId);


        Task SaveChangesAsync();
    }
}
