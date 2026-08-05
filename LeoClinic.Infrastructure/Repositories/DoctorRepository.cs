using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeoClinic.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext context;
        public DoctorRepository(AppDbContext context)
        {
            this.context = context;
        }
        //public async Task<IEnumerable<DoctorProfile>> SearchAsync(string? specialty, int? locationId, string? name)
        //{
        //    var query = context.DoctorProfiles
        //        .Include(d => d.User)
        //        .Include(d => d.Speciality)
        //        .Include(d => d.DoctorLocations).ThenInclude(dl => dl.Location)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(specialty))
        //    {
        //        query = query.Where(d => d.Speciality.Name.Contains(specialty));
        //    }

        //    if (locationId.HasValue)
        //    {
        //        query = query.Where(d => d.DoctorLocations.Any(dl => dl.LocationId == locationId.Value));
        //    }

        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(d => d.User.FirstName.Contains(name) || d.User.LastName.Contains(name));
        //    }

        //    return await query.ToListAsync();
        //}
        public async Task<Availability> AddSlotAsync(Availability slot)
        {
            await context.Availabilities.AddAsync(slot);
            return slot;
        }

        public async Task<DoctorProfile> CreateAsync(DoctorProfile doctorProfile)
        {
            await context.DoctorProfiles.AddAsync(doctorProfile);
            return doctorProfile;
        }

        public void Delete(DoctorProfile doctorProfile)
        {
            context.DoctorProfiles.Remove(doctorProfile);
        }

        public async Task<IEnumerable<DoctorProfile>> GetAllAsync()
        {
            var doctors = await context.DoctorProfiles
                .Include(d => d.User)
                .Include(d => d.Speciality)
                .Include(d => d.DoctorLocations).ThenInclude(dl => dl.Location)
                .Include(d => d.Ratings)
                .ToListAsync();

            return doctors;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await context.Appointments
                .Include(a => a.PatientProfile).ThenInclude(p => p.User)
                .Include(a => a.DoctorProfile).ThenInclude(d => d.User)
                .Include(a => a.Availability).ThenInclude(av => av.Location)
                .Include(a => a.Payment)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
            if (appointment is null) return null;

            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(int doctorId)
        {
            var appointments = await context.Appointments
                .Include(a => a.Availability).ThenInclude(av => av.Location)
                .Include(a => a.PatientProfile).ThenInclude(p => p.User)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            return appointments;
        }

        public async Task<DoctorProfile?> GetByIdAsync(int id)
        {
            var doctor = await context.DoctorProfiles
                .Include(d => d.User)
                .Include(d => d.Speciality)
                .Include(d => d.DoctorLocations).ThenInclude(dl => dl.Location)
                .Include(d => d.Ratings)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is null) return null;

            return doctor;
        }

        public async Task<IEnumerable<Rating>> GetReviewsByDoctorIdAsync(int doctorId)
        {
            var rate = await context.Ratings
                .Include(r => r.PatientProfile).ThenInclude(p => p.User)
                .Where(u => u.DoctorId == doctorId)
                .ToListAsync();
            return rate;
        }

        public async Task<IEnumerable<Availability>> GetSlotsByDoctorIdAsync(int doctorId)
        {
            var slots = await context.Availabilities
                .Include(a => a.Location)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            return slots;
        }

        public  void Update(DoctorProfile doctorProfile)
        {
            context.DoctorProfiles.Update(doctorProfile);
        }

        public Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            context.Appointments.Update(appointment);
            return Task.FromResult(appointment);
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorProfile>> GetApprovedDoctorsAsync()
        {
            var approved = await context.DoctorProfiles
                        .Include(d => d.User)
                        .Include(d => d.Speciality)
                        .Include(d => d.DoctorLocations).ThenInclude(dl => dl.Location)
                        .Include(d => d.Ratings)
                        .Where(d => d.IsApproved)
                        .ToListAsync();
            return approved;
        }
    }
}
