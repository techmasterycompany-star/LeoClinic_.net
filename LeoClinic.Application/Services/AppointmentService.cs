using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;


namespace LeoClinic.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IAvailabilityRepository _availabilityRepository;
        public AppointmentService(IAppointmentRepository appointmentRepo, IAvailabilityRepository availabilityRepository)
        {
            _appointmentRepo = appointmentRepo;
            _availabilityRepository = availabilityRepository;
        }
        public async Task<AppointmentDto> BookAppointmentAsync(CreateAppointmentDTO appointment)
        {
            var app = new Appointment
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AvailabilityId = appointment.AvailabilityId,
                Notes = appointment.Notes,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            };

            var bookedAppointment = await _appointmentRepo.BookAppointmentAsync(app);


            var result = new AppointmentDto
            {
                Id = bookedAppointment.Id,
                Status = bookedAppointment.Status,
                Notes = bookedAppointment.Notes,
                PatientName = bookedAppointment.PatientProfile?.User != null
                    ? $"{bookedAppointment.PatientProfile.User.FirstName} {bookedAppointment.PatientProfile.User.LastName}"
                    : string.Empty,
                DoctorName = bookedAppointment.DoctorProfile?.User != null
                    ? $"{bookedAppointment.DoctorProfile.User.FirstName} {bookedAppointment.DoctorProfile.User.LastName}"
                    : string.Empty,
                Date = bookedAppointment.Availability?.Date ?? DateTime.MinValue,
                StartTime = bookedAppointment.Availability?.StartTime ?? TimeSpan.Zero,
                LocationName = bookedAppointment.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = bookedAppointment.Payment?.Amount
            };
            return result;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllByPatientAsync(int patientId)
        {
            var appointments = await _appointmentRepo.GetAllByPatientAsync(patientId);
            var result = appointments.Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Notes = appointment.Notes,
                PatientName = appointment.PatientProfile?.User != null
                    ? $"{appointment.PatientProfile.User.FirstName} {appointment.PatientProfile.User.LastName}"
                    : string.Empty,
                DoctorName = appointment.DoctorProfile?.User != null
                    ? $"{appointment.DoctorProfile.User.FirstName} {appointment.DoctorProfile.User.LastName}"
                    : string.Empty,
                Date = appointment.Availability?.Date ?? DateTime.MinValue,
                StartTime = appointment.Availability?.StartTime ?? TimeSpan.Zero,
                EndTime = appointment.Availability?.EndTime ?? TimeSpan.Zero,
                LocationName = appointment.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = appointment.Payment?.Amount
            });
            
            return result;
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return null;
            }
            return new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Notes = appointment.Notes,
                PatientName = appointment.PatientProfile?.User != null
                    ? $"{appointment.PatientProfile.User.FirstName} {appointment.PatientProfile.User.LastName}"
                    : string.Empty,
                DoctorName = appointment.DoctorProfile?.User != null
                    ? $"{appointment.DoctorProfile.User.FirstName} {appointment.DoctorProfile.User.LastName}"
                    : string.Empty,
                Date = appointment.Availability?.Date ?? DateTime.MinValue,
                StartTime = appointment.Availability?.StartTime ?? TimeSpan.Zero,
                EndTime = appointment.Availability?.EndTime ?? TimeSpan.Zero,
                LocationName = appointment.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = appointment.Payment?.Amount
            };
        }


        public async Task<bool> RescheduleAppointment(int id, int newAvailabilityId)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            var oldSlot = appointment.Availability;
            var newSlot = await _availabilityRepository.GetSlotByIdAsync(newAvailabilityId);

            if (newSlot == null || oldSlot == null)
            {
                return false;
            }

            if (newSlot.DoctorId != oldSlot.DoctorId)
                throw new InvalidOperationException("Cannot reschedule to a different doctor's slot.");

            if (newSlot.IsBooked)
                throw new InvalidOperationException("This availability slot is already booked");

            oldSlot.IsBooked = false;
            oldSlot.UpdatedAt = DateTime.UtcNow;
            newSlot.IsBooked = true;
            newSlot.UpdatedAt = DateTime.UtcNow;

            appointment.AvailabilityId = newAvailabilityId;
            appointment.UpdatedAt = DateTime.UtcNow;
            await _appointmentRepo.UpdateAppointmentAsync(appointment);
            return true;
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int id, AppointmentStatus status)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            appointment.Status = status;
            appointment.UpdatedAt = DateTime.UtcNow;
            if(status == AppointmentStatus.Cancelled || status == AppointmentStatus.Rejected)
            {
                if (appointment.Availability != null)
                {
                    appointment.Availability.IsBooked = false;
                    appointment.Availability.UpdatedAt = DateTime.UtcNow;
                }
            }
            await _appointmentRepo.UpdateAppointmentAsync(appointment);
            return true;
        }
    }
}
