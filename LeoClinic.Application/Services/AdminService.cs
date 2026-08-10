using LeoClinic.Application.DTOs.Admin;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LeoClinic.Application.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            var specialties = await _adminRepository.GetAllSpecialtiesAsync();

            return specialties.Select(s => new SpecialtyDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description

            });
        }
        public async Task<SpecialtyDto?> GetSpecialtyByIdAsync(int id)
        {
            var speciality = await _adminRepository.GetSpecialtyByIdAsync(id);

            if (speciality == null)
                return null;
            return new SpecialtyDto
            {
                Id = speciality.Id,
                Name = speciality.Name,
                Description = speciality.Description
            };
        }
        public async Task CreateSpecialtyAsync(CreateSpecialtyDto dto)
        {
            var specialty = new Speciality
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _adminRepository.CreateSpecialtyAsync(specialty);
        }
        public async Task<bool> UpdateSpecialtyAsync(int id, UpdateSpecialtyDto dto)
        {
            var specialty = await _adminRepository.GetSpecialtyByIdAsync(id);

            if (specialty == null)
                return false;

            specialty.Name = dto.Name;
            specialty.Description = dto.Description;

            await _adminRepository.UpdateSpecialityAsync(id, specialty);

            return true;
        }
        public async Task<bool> DeleteSpecialtyAsync(int id)
        {
            try { 
            var specialty = await _adminRepository.GetSpecialtyByIdAsync(id);

            if (specialty == null)
                return false;

            await _adminRepository.DeleteSpecialtyAsync(id);

            return true;
            }
            catch (DbUpdateException) { throw new InvalidOperationException(
                "This specialty is related to one or more doctors. Please remove or reassign the doctors first."); }
        }



        //implement the methods for approving and rejecting doctors
        public async Task<IEnumerable<PendingDoctorDto>> GetPendingDoctorsAsync()
        {
            var pendingDoctors = await _adminRepository.GetPendingDoctorsAsync();

            return pendingDoctors.Select(d => new PendingDoctorDto
            {
               Id = d.Id,
              FullName = $"{d.User.FirstName} {d.User.LastName}".Trim(),
                Email = d.User.Email,
                ContactNumber = d.ContactNumber,
                Specialty = d.Speciality.Name,
                Price = d.Price,
                Bio = d.Bio,
                DateJoined = d.User.DateJoined
            });
        }
        public async Task<bool> ApproveDoctorAsync(int id)
        {
            var doctor = await _adminRepository.GetDoctorByIdAsync(id);

            if (doctor == null || doctor.IsApproved)
                return false;

            doctor.IsApproved = true;

            await _adminRepository.UpdateDoctorAsync(doctor);

            return true;
        }
        public async Task<bool> RejectDoctorAsync(int id)
        {
            var doctor = await _adminRepository.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsApproved)
                return false;

            doctor.IsApproved = false;

            await _adminRepository.UpdateDoctorAsync(doctor);

            return true;
        }


        // Implement the methods for managing locations
        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _adminRepository.GetAllLocationsAsync();

            return locations.Select(l => new LocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address,
                City = l.City,
                Phone = l.Phone
            });
        }
        public async Task<LocationDto?> GetLocationByIdAsync(int id)
        {
            var location = await _adminRepository.GetLocationByIdAsync(id);

            if (location == null)
                return null;

            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                City = location.City,
                Phone = location.Phone
            };
        }
        public async Task CreateLocationAsync(CreateLocationDto dto)
        {
            var location = new Location
            {
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Phone = dto.Phone
            };

            await _adminRepository.CreateLocationAsync(location);
        }
        public async Task<bool> UpdateLocationAsync(int id, UpdateLocationDto dto)
        {
            var location = await _adminRepository.GetLocationByIdAsync(id);

            if (location == null)
                return false;

            location.Name = dto.Name;
            location.Address = dto.Address;
            location.City = dto.City;
            location.Phone = dto.Phone;

            await _adminRepository.UpdateLocationAsync(location);

            return true;
        }
        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _adminRepository.GetLocationByIdAsync(id);

            if (location == null)
                return false;

            await _adminRepository.DeleteLocationAsync(location);

            return true;
        }


        // Implement the methods for managing doctor locations
        public async Task<IEnumerable<LocationDto>> GetDoctorLocationsAsync(int doctorId)
        {
            var locations = await _adminRepository.GetDoctorLocationsAsync(doctorId);

            return locations.Select(l => new LocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address,
                City = l.City,
                Phone = l.Phone
            });
        }
        public async Task<IEnumerable<DoctorLocationDto>> GetLocationDoctorsAsync(int locationId)
        {
            var doctors = await _adminRepository.GetLocationDoctorsAsync(locationId);

            return doctors.Select(d => new DoctorLocationDto
            {
                Id = d.Id,
                FullName = $"{d.User.FirstName} {d.User.LastName}",
                Email = d.User.Email,
                Specialty = d.Speciality.Name,
                ContactNumber = d.ContactNumber
            });
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(AppointmentStatus? status,int? doctorId,int? patientId,DateTime? date)
        {
            var appointments = await _adminRepository.GetAppointmentsAsync(status,doctorId,patientId,date);

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                Status = a.Status,
                Notes = a.Notes,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AvailabilityId = a.AvailabilityId,
                Date = a.Availability.Date,
                StartTime = a.Availability.StartTime,
                EndTime = a.Availability.EndTime
            });
        }

        public async Task<bool> CancelAppointmentAsync(int id)
        {
            var appointment = await _adminRepository.GetAppointmentByIdAsync(id);

            if (appointment == null)
                return false;

            if (appointment.Status == AppointmentStatus.Cancelled)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;

            await _adminRepository.UpdateAppointmentStatusAsync(appointment);

            return true;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(string? email,UserRole? role, bool? isBlocked)
        {
            var users = await _adminRepository.GetUsersAsync(email,role,isBlocked);

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Role = u.Role,
                IsBlocked = u.IsBlocked,
                DateJoined = u.DateJoined
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                IsBlocked = user.IsBlocked,
                DateJoined = user.DateJoined
            };
        }

        public async Task<bool> BlockUserAsync(int id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;
            if (user.IsBlocked)
                return false;
            user.IsBlocked = true;
            await _adminRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> UnblockUserAsync(int id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;
            if (!user.IsBlocked)
                return false;
            user.IsBlocked = false;
            await _adminRepository.UpdateUserAsync(user);
            return true;
        }

        //Dashboard & Revenue statistics
        public async Task<DashboardDto> GetDashboardStatisticsAsync()
        {
            var totalUsers = await _adminRepository.GetTotalUsersAsync();

            var totalDoctors = await _adminRepository.GetTotalDoctorsAsync();

            var totalPatients = await _adminRepository.GetTotalPatientsAsync();

            var pendingDoctors = await _adminRepository.GetTotalPendingDoctorsAsync();

            var totalAppointments = await _adminRepository.GetTotalAppointmentsAsync();

            var completedAppointments =
                await _adminRepository.GetCompletedAppointmentsAsync();

            var cancelledAppointments =
                await _adminRepository.GetCancelledAppointmentsAsync();

            var totalRevenue =
                await _adminRepository.GetTotalRevenueAsync();

            return new DashboardDto
            {
                TotalUsers = totalUsers,
                TotalDoctors = totalDoctors,
                TotalPatients = totalPatients,
                PendingDoctors = pendingDoctors,
                TotalAppointments = totalAppointments,
                CompletedAppointments = completedAppointments,
                CancelledAppointments = cancelledAppointments,
                TotalRevenue = totalRevenue
            };
        }
        public async Task<RevenueDto> GetRevenueAsync()
        {
            var totalRevenue = await _adminRepository.GetTotalRevenueAsync();

            var totalPayments = await _adminRepository.GetTotalPaymentsAsync();

            return new RevenueDto
            {
                TotalRevenue = totalRevenue,
                TotalPayments = totalPayments
            };
        }
        public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync(int? doctorId, int? patientId, DateTime? date)
        {
            var payments = await _adminRepository.GetPaymentAsync(doctorId, patientId, date);

            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                PaymentMethod = p.PaymentMethod,
                PaymentDate = p.PaymentDate,
                Amount = p.Amount,
                PatientId = p.PatientId,
                AppointmentId = p.AppointmentId,
                DoctorId = p.Appointment.DoctorId
            });
        }
        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _adminRepository.GetPaymentByIdAsync(id);

            if (payment == null)
            {
                return null;
            }

            return new PaymentDto
            {
                Id = payment.Id,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,
                PatientId = payment.PatientId,
                AppointmentId = payment.AppointmentId,
                DoctorId = payment.Appointment.DoctorId
            };
        }
        public async Task<IEnumerable<RatingDto>> GetRatingsAsync(int? doctorId,int? patientId, int? rate)
        {
            var ratings = await _adminRepository.GetRatingsAsync(doctorId,patientId, rate);

            return ratings.Select(r => new RatingDto
            {
                Id = r.Id,
                Rate = r.Rate,
                Review = r.Review,
                DoctorId = r.DoctorId,
                PatientId = r.PatientId
            });
        }
        public async Task<RatingDto?> GetRatingByIdAsync(int id)
        {
            var rating = await _adminRepository.GetRatingByIdAsync(id);

            if (rating == null)
            {
                return null;
            }

            return new RatingDto
            {
                Id = rating.Id,
                Rate = rating.Rate,
                Review = rating.Review,
                DoctorId = rating.DoctorId,
                PatientId = rating.PatientId
            };
        }
        public async Task<bool> DeleteRatingAsync(int id)
        {
            return await _adminRepository.DeleteRatingAsync(id);
        }
    }
}
