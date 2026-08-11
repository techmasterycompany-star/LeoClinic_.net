using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _dbContext;
        public AppointmentRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Appointment> BookAppointmentAsync(Appointment appointment)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var availability = await _dbContext.Availabilities
                    .FromSqlInterpolated($@"
                        SELECT *
                        FROM Availabilities WITH (UPDLOCK, ROWLOCK)
                        WHERE Id = {appointment.AvailabilityId}"
                    )
                    .SingleOrDefaultAsync();

                if (availability == null)
                {
                    throw new KeyNotFoundException("Availability slot not found.");
                }
                if (availability.IsBooked)
                {
                    throw new InvalidOperationException("This availability slot is already booked");
                }

                appointment.DoctorId = availability.DoctorId;

                _dbContext.Appointments.Add(appointment);

                availability.IsBooked = true;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return await _dbContext.Appointments
                    .Include(a => a.PatientProfile).ThenInclude(p => p.User)
                    .Include(a => a.DoctorProfile).ThenInclude(d => d.User)
                    .Include(a => a.Availability).ThenInclude(av => av.Location)
                    .Include(a => a.Payment)
                    .SingleAsync(a => a.Id == appointment.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllByPatientAsync(int patientId)
        {
            var appointments = await _dbContext.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.DoctorProfile).ThenInclude(d => d.User)
                .Include(a => a.Availability).ThenInclude(av => av.Location)
                .Include(a => a.PatientProfile).ThenInclude(p => p.User)
                .Include(a => a.Payment)
                .ToListAsync();
            return appointments;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _dbContext.Appointments
                .Include(a => a.DoctorProfile).ThenInclude(d => d.User)
                .Include(a => a.Availability).ThenInclude(av => av.Location)
                .Include(a => a.PatientProfile).ThenInclude(p => p.User)
                .Include(a => a.Payment)
                .FirstOrDefaultAsync(a => a.Id == id);
            return appointment;
        }

        public async Task<bool> hasCompletedAppointment(int patientId, int doctorId)
        {
            var doctorExists = await _dbContext.DoctorProfiles.AnyAsync(d => d.Id == doctorId);
            if (!doctorExists)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }
            return await _dbContext.Appointments.AnyAsync(a => a.PatientId == patientId && a.DoctorId == doctorId && a.Status == AppointmentStatus.Completed);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
