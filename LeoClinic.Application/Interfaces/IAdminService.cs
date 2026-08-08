using LeoClinic.Application.DTOs.Admin;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();
        Task<SpecialtyDto?> GetSpecialtyByIdAsync(int id);
        Task CreateSpecialtyAsync(CreateSpecialtyDto dto);
        Task<bool> UpdateSpecialtyAsync(int id, UpdateSpecialtyDto dto);
        Task<bool> DeleteSpecialtyAsync(int id);


        Task<IEnumerable<PendingDoctorDto>> GetPendingDoctorsAsync();
        Task<bool> ApproveDoctorAsync(int id);
        Task<bool> RejectDoctorAsync(int id);


        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto?> GetLocationByIdAsync(int id);
        Task CreateLocationAsync(CreateLocationDto dto);
        Task<bool> UpdateLocationAsync(int id, UpdateLocationDto dto);
        Task<bool> DeleteLocationAsync(int id);


        Task<IEnumerable<LocationDto>> GetDoctorLocationsAsync(int doctorId);
        Task<IEnumerable<DoctorLocationDto>> GetLocationDoctorsAsync(int locationId);


        Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(AppointmentStatus? status, int? doctorId, int? patientId, DateTime? date);
        Task<bool> CancelAppointmentAsync(int id);

        Task<IEnumerable<UserDto>> GetUsersAsync(string? email,UserRole? role,bool? isBlocked);

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<bool> BlockUserAsync(int id);

        Task<bool> UnblockUserAsync(int id);
        Task<DashboardDto> GetDashboardStatisticsAsync();
        Task<RevenueDto> GetRevenueAsync();
        Task<IEnumerable<PaymentDto>> GetPaymentsAsync(int? doctorId, int? patientId, DateTime? date);
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<RatingDto>> GetRatingsAsync(int? doctorId,int? patientId, int? rate);
        Task<RatingDto?> GetRatingByIdAsync(int id);
        Task<bool> DeleteRatingAsync(int id);

    }
}
