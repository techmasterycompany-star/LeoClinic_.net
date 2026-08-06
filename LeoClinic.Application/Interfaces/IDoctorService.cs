using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorProfileDto?>> GetAllDoctorsAsync();
        Task<DoctorProfileDto?> GetDoctorProfileAsync(int id);
        Task<IEnumerable<DoctorProfileDto?>> SearchDoctorAsync(string? specialty, int? locationId, string? name, bool? isApproved);
        Task<DoctorProfileDto> CreateProfileAsync(CreateDoctorDto dto);
        Task<DoctorProfileDto?> UpdateProfileAsync(int id ,UpdateDoctorDto dto);
        Task<bool> DeleteProfileAsync(int id);
        Task<bool> ApproveDoctorAsync(int id);
        Task<bool> RejectDoctorAsync(int id);
        Task<IEnumerable<DoctorProfileDto>> GetApprovedDoctorsAsync();


        Task<AvailabilityDto> CreateSlotAsync(CreateAvailabilityDto dto);
        Task<IEnumerable<AvailabilityDto>> GetSlotsByDoctorIdAsync(int doctorId);


        Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(int doctorId);
        Task<AppointmentDto?> GetAppointmentByIdAsync(int appointmentId);
        Task<AppointmentDto?> UpdateAppointmentAsync(int appointmentId, AppointmentStatus status);


        Task<IEnumerable<RatingDto>> GetReviewsByDoctorIdAsync(int doctorId);

    }
}
