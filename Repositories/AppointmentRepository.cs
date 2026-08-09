using Clinic.Data;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PatientExistsAsync(int patientId)
        {
            return await _context.PatientProfiles
                .AnyAsync(p => p.Id == patientId);
        }

        public async Task<bool> DoctorExistsAsync(int doctorId)
        {
            return await _context.DoctorProfiles
                .AnyAsync(d => d.Id == doctorId);
        }

        public async Task<Availability?> GetAvailabilityAsync(int availabilityId)
        {
            return await _context.Availabilities
                .FirstOrDefaultAsync(a => a.Id == availabilityId);
        }

        public async Task UpdateAvailabilityAsync(Availability availability)
        {
            _context.Availabilities.Update(availability);

            await _context.SaveChangesAsync();
        }

        public async Task<Appointment?> GetAppointmentWithAvailabilityAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Availability)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }
    }
}
