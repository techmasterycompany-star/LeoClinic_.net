using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeoClinic.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        // Implement methods for managing specialties
        public async Task<IEnumerable<Speciality>> GetAllSpecialtiesAsync()
        {
            return await _context.Specialties.ToListAsync();
        }
        public async Task<Speciality?> GetSpecialtyByIdAsync(int id)
        {
            return await _context.Specialties.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task CreateSpecialtyAsync(Speciality specialty)
        {
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateSpecialityAsync(int id, Speciality updatedSpecialty)
        {
            var specialtyFromDB = await _context.Specialties.FindAsync(id);
            if (specialtyFromDB != null)
            {
                specialtyFromDB.Name = updatedSpecialty.Name;
                specialtyFromDB.Description = updatedSpecialty.Description;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteSpecialtyAsync(int id)
        {
            var specialtyFromDB = await _context.Specialties.FindAsync(id);
            if (specialtyFromDB != null)
            {
                _context.Specialties.Remove(specialtyFromDB);
                await _context.SaveChangesAsync();
            }

        }


        // Implement methods for managing doctor approvals
        public async Task<IEnumerable<DoctorProfile>> GetPendingDoctorsAsync()
        {
            var pendingDoctors = await _context.DoctorProfiles
                .Include(d=>d.User)
                .Include(d=>d.Speciality)
                .Where(d => d.IsApproved == false)
                .ToListAsync();

            return pendingDoctors;
        }

        public async Task<DoctorProfile?> GetDoctorByIdAsync(int id)
        {
            return await _context.DoctorProfiles.FindAsync(id);

        }
        public async Task UpdateDoctorAsync(DoctorProfile doctor)
        {
            _context.DoctorProfiles.Update(doctor);
            await _context.SaveChangesAsync();
        }

        // Implement methods for managing locations
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }
        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }
        public async Task CreateLocationAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLocationAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLocationAsync(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location>> GetDoctorLocationsAsync(int doctorId)
        {
            var doctorLocations = await _context.DoctorLocations
                .Where(dl => dl.DoctorId == doctorId)
                .Select(dl => dl.Location)
                .ToListAsync();

            return doctorLocations;
        }
        public async Task<IEnumerable<DoctorProfile>> GetLocationDoctorsAsync(int locationId)
        {
            var locationDoctors = await _context.DoctorLocations
                .Where(dl => dl.LocationId == locationId) //Filter by the specified location ID
                .Include(dp=>dp.DoctorProfile) 
                .ThenInclude(d=>d.User)
                .Include(dp=>dp.DoctorProfile)
                .ThenInclude(d=>d.Speciality)
                .Select(dl => dl.DoctorProfile)
                .ToListAsync();

            return locationDoctors;
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(AppointmentStatus? status,int? doctorId,int? patientId,DateTime? date)
        {
            var query = _context.Appointments
                .Include(a => a.PatientProfile)
                .Include(a => a.DoctorProfile)
                .Include(a => a.Availability)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            if (doctorId.HasValue)
            {
                query = query.Where(a => a.DoctorId == doctorId.Value);
            }

            if (patientId.HasValue)
            {
                query = query.Where(a => a.PatientId == patientId.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(a => a.Availability.Date.Date == date.Value.Date);
            }

            return await query.ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
           .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAppointmentStatusAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string? email, UserRole? role, bool? isBlocked)
        {
            var userquery = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
            {
                userquery = userquery.Where(u => u.Email.Contains(email));
            }

            if (role.HasValue)
            {
                userquery = userquery.Where(u => u.Role == role.Value);
            }

            if (isBlocked.HasValue)
            {
                userquery = userquery.Where(u => u.IsBlocked == isBlocked.Value);
            }

            return await userquery.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalDoctorsAsync()
        {
            return await _context.DoctorProfiles.CountAsync();
        }

        public async Task<int> GetTotalPatientsAsync()
        {
            return await _context.PatientProfiles.CountAsync();
        }
        public async Task<int> GetTotalPendingDoctorsAsync()
        {
            return await _context.DoctorProfiles
                .CountAsync(d => !d.IsApproved);
        }

        public async Task<int> GetTotalAppointmentsAsync()
        {
            return await _context.Appointments.CountAsync();
        }

        public async Task<int> GetCompletedAppointmentsAsync()
        {
            return await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Completed);
        }

        public async Task<int> GetCancelledAppointmentsAsync()
        {
            return await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Cancelled);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Payments.SumAsync(p => p.Amount);
        }
        public async Task<int> GetTotalPaymentsAsync()
        {
            return await _context.Payments.CountAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentAsync(int? doctorId, int? patientId,DateTime? date)
        {
            var query = _context.Payments
                .Include(p => p.Appointment)
                .AsQueryable();

            if (patientId.HasValue)
            {
                query = query.Where(p => p.PatientId == patientId.Value);
            }

            if (doctorId.HasValue)
            {
                query = query.Where(p => p.Appointment.DoctorId == doctorId.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(p => p.PaymentDate.Date == date.Value.Date);
            }

            return await query.ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
           return await _context.Payments
                .Include(x=>x.Appointment)
                .Include(p=>p.PatientProfile)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Rating>> GetRatingsAsync(int? doctorId,int? patientId,int? rate)
        {
            var query = _context.Ratings.AsQueryable();

            if (doctorId.HasValue)
            {
                query = query.Where(r => r.DoctorId == doctorId.Value);
            }

            if (patientId.HasValue)
            {
                query = query.Where(r => r.PatientId == patientId.Value);
            }
            if (rate.HasValue)
            {
                query = query.Where(r => r.Rate == rate.Value);
            }

            return await query.ToListAsync();
        }
        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);

        }
        public async Task<bool> DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return false;
            }
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

