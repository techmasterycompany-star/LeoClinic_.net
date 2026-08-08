using LeoClinic.Application.DTOs.Admin;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface IAdminRepository
    {
        // Define methods for managing specialties
        Task<IEnumerable<Speciality>> GetAllSpecialtiesAsync();
        Task<Speciality?> GetSpecialtyByIdAsync(int id);
        Task CreateSpecialtyAsync(Speciality specialty);
        Task UpdateSpecialityAsync(int id, Speciality specialty);
        Task DeleteSpecialtyAsync(int id);

        // Define methods for managing doctor approvals
        Task<IEnumerable<DoctorProfile>> GetPendingDoctorsAsync();

        Task<DoctorProfile?> GetDoctorByIdAsync(int id);

        Task UpdateDoctorAsync(DoctorProfile doctor);

        // Define methods for managing locations
        Task<IEnumerable<Location>> GetAllLocationsAsync();

        Task<Location?> GetLocationByIdAsync(int id);

        Task CreateLocationAsync(Location location);

        Task UpdateLocationAsync(Location location);

        Task DeleteLocationAsync(Location location);
        Task<IEnumerable<Location>> GetDoctorLocationsAsync(int doctorId);

        Task<IEnumerable<DoctorProfile>> GetLocationDoctorsAsync(int locationId);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync(AppointmentStatus? status,int? doctorId,int? patientId,DateTime? date);
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task UpdateAppointmentStatusAsync(Appointment appointment);


        Task<IEnumerable<User>> GetUsersAsync(string? email,UserRole? role,bool? isBlocked);
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<int> GetTotalUsersAsync();

        Task<int> GetTotalDoctorsAsync();

        Task<int> GetTotalPatientsAsync();

        Task<int> GetTotalPendingDoctorsAsync();

        Task<int> GetTotalAppointmentsAsync();

        Task<int> GetCompletedAppointmentsAsync();

        Task<int> GetCancelledAppointmentsAsync();

        Task<decimal> GetTotalRevenueAsync();

        Task<IEnumerable<Payment>> GetPaymentAsync(int? doctorId, int? patientId, DateTime? date);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<int> GetTotalPaymentsAsync();
        Task<IEnumerable<Rating>> GetRatingsAsync(int? doctorId,int? patientId,int? rate);

        Task<Rating?> GetRatingByIdAsync(int id);

        Task<bool> DeleteRatingAsync(int id);


    }
}
