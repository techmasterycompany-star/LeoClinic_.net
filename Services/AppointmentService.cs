using Clinic.DTOs;
using Clinic.Models;
using Clinic.Repositories;
using Clinic.Exceptions;

namespace Clinic.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INotificationService _notificationService;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            INotificationService notificationService)
        {
            _appointmentRepository = appointmentRepository;
            _notificationService = notificationService;
        }

        public async Task<Appointment> BookAppointmentAsync(BookAppointmentDto dto)
        {
            if (!await _appointmentRepository.PatientExistsAsync(dto.PatientId))
            {
                throw new NotFoundException("Patient not found.");
            }

            if (!await _appointmentRepository.DoctorExistsAsync(dto.DoctorId))
            {
                throw new NotFoundException("Doctor not found.");
            }

            var availability = await _appointmentRepository.GetAvailabilityAsync(dto.AvailabilityId);

            if (availability == null)
            {
                throw new NotFoundException("Availability not found.");
            }

            if (availability.IsBooked)
            {
                throw new BadRequestException("This appointment slot is already booked.");
            }


            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AvailabilityId = dto.AvailabilityId,
                Status = AppointmentStatus.Pending,
                Notes = dto.Notes,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };


            appointment = await _appointmentRepository.CreateAppointmentAsync(appointment);

            availability.IsBooked = true;
            await _appointmentRepository.UpdateAvailabilityAsync(availability);


            await _notificationService.SendNotificationAsync(
                dto.PatientId,
                appointment.Id,
                "Your appointment has been booked successfully.");

            await _notificationService.SendNotificationAsync(
                dto.DoctorId,
                appointment.Id,
                "A new appointment has been booked.");

            return appointment;
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAvailabilityAsync(appointmentId);

            if (appointment == null)
                throw new NotFoundException("Appointment not found.");

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.UpdatedAt = DateTime.Now;

            appointment.Availability.IsBooked = false;

            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            await _appointmentRepository.UpdateAvailabilityAsync(appointment.Availability);

            await _notificationService.SendNotificationAsync(
                appointment.PatientId,
                appointment.Id,
                "Your appointment has been cancelled.");

            await _notificationService.SendNotificationAsync(
                appointment.DoctorId,
                appointment.Id,
                "The appointment has been cancelled.");
        }
    }
}
