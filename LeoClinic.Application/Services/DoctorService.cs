using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository repo;
        public DoctorService(IDoctorRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> ApproveDoctorAsync(int id)
        {
            var doctor = await repo.GetByIdAsync(id);
            if(doctor is null) return false;

            doctor.IsApproved = true;
            doctor.UpdatedAt = DateTime.UtcNow;

            repo.Update(doctor);
            await repo.SaveChangesAsync();
            return true;
        }

        //public async Task<DoctorProfileDto> CreateProfileAsync(CreateDoctorDto dto)
        //{
        //    var doctor = new DoctorProfile
        //    {
        //        Price = dto.Price,
        //        Bio = dto.Bio,
        //        ContactNumber = dto.ContactNumber,
        //        SpecialityId = dto.SpecialityId,
        //        UserId = dto.UserId,
        //        IsApproved = false,
        //        CreatedAt = DateTime.UtcNow
        //    };
        //    await repo.CreateAsync(doctor);

        //    if (dto.LocationIds != null && dto.LocationIds.Any())
        //    {
        //        foreach (var locationId in dto.LocationIds)
        //        {
        //            doctor.DoctorLocations.Add(new DoctorLocation
        //            {
        //                DoctorId = doctor.Id,
        //                LocationId = locationId,
        //                CreatedAt = DateTime.UtcNow
        //            });
        //        }
        //    }

        //    var profile = new DoctorProfileDto
        //    {
        //        Id = doctor.Id,
        //        Price = doctor.Price,
        //        Bio = doctor.Bio,
        //        ContactNumber = doctor.ContactNumber,
        //        IsApproved = doctor.IsApproved,
        //        SpecialityId = doctor.SpecialityId
        //    };
        //    await repo.SaveChangesAsync();
        //    return profile;
        //}

        public async Task<IEnumerable<AvailabilityDto>> CreateSlotAsync(CreateAvailabilityDto dto)
        {
            if (dto.SlotDurationMinutes <= 0)
                throw new ArgumentException("Slot duration must be greater than 0.");

            if (dto.DayStartTime >= dto.DayEndTime)
                throw new ArgumentException("Start time must be before end time.");

            var slots = new List<Availability>();
            var currentStart = dto.DayStartTime;
            var duration = TimeSpan.FromMinutes(dto.SlotDurationMinutes);

            while (currentStart + duration <= dto.DayEndTime)
            {
                var slot = new Availability
                {
                    DoctorId = dto.DoctorId,
                    LocationId = dto.LocationId,
                    Date = dto.Date,
                    StartTime = currentStart,
                    EndTime = currentStart + duration,
                    IsBooked = false,
                    CreatedAt = DateTime.UtcNow
                };

                slots.Add(slot);
                await repo.AddSlotAsync(slot);
                currentStart += duration;
            }

            await repo.SaveChangesAsync();

            var location = await repo.GetLocationByIdAsync(dto.LocationId);

            return slots.Select(s => new AvailabilityDto
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                IsBooked = s.IsBooked,
                Location = location != null
                    ? new LocationDto
                    {
                        Id = location.Id,
                        Name = location.Name,
                        Address = location.Address,
                        City = location.City
                    }
                    : new LocationDto { Id = dto.LocationId }
            }).ToList();
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            var doctor = await repo.GetByIdAsync(id);
            if (doctor is null) return false;

            if (doctor.Ratings.Any())
                repo.RemoveRatings(doctor.Ratings);

            if (doctor.Appointments.Any())
                repo.RemoveAppointments(doctor.Appointments);

            if (doctor.Availabilities.Any())
                repo.RemoveAvailabilities(doctor.Availabilities);

            repo.Delete(doctor);
            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DoctorProfileDto?>> GetAllDoctorsAsync()
        {
            var doctors = await repo.GetAllAsync();

            return doctors.Select(d => new DoctorProfileDto
            {
                Id = d.Id,
                Price = d.Price,
                Bio = d.Bio,
                ContactNumber = d.ContactNumber,
                IsApproved = d.IsApproved,
                UserId = d.UserId,
                UserName = d.User != null ? $"{d.User.FirstName} {d.User.LastName}" : string.Empty,
                UserEmail = d.User?.Email ?? string.Empty,
                SpecialityId = d.SpecialityId,
                SpecialityName = d.Speciality?.Name ?? string.Empty,
                Locations = d.DoctorLocations?.Select(dl => new LocationDto
                {
                    Id = dl.Location.Id,
                    Name = dl.Location.Name,
                    Address = dl.Location.Address,
                    City = dl.Location.City
                }).ToList() ?? new List<LocationDto>(),
                AverageRating = d.Ratings?.Any() == true ? d.Ratings.Average(r => r.Rate) : 0
            }).ToList();
        }

        public async Task<IEnumerable<DoctorProfileDto?>> SearchDoctorAsync(string? specialty, int? locationId, string? name)
        {
            var doctors = await repo.SearchAsync(specialty, locationId, name);    

            return doctors.Select(d => new DoctorProfileDto
            {
                Id = d.Id,
                Price = d.Price,
                Bio = d.Bio,
                ContactNumber = d.ContactNumber,
                IsApproved = d.IsApproved,
                UserId = d.UserId,
                UserName = d.User != null ? $"{d.User.FirstName} {d.User.LastName}" : string.Empty,
                UserEmail = d.User?.Email ?? string.Empty,
                SpecialityId = d.SpecialityId,
                SpecialityName = d.Speciality?.Name ?? string.Empty,
                Locations = d.DoctorLocations?.Select(dl => new LocationDto
                {
                    Id = dl.Location.Id,
                    Name = dl.Location.Name,
                    Address = dl.Location.Address,
                    City = dl.Location.City
                }).ToList() ?? new List<LocationDto>(),
                AverageRating = d.Ratings?.Any() == true ? d.Ratings.Average(r => r.Rate) : 0
            }).ToList();
        }


        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await repo.GetAppointmentByIdAsync(appointmentId);
            if (appointment is null)
                return null;

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
                DoctorId = appointment.DoctorId,
                Date = appointment.Availability?.Date ?? DateTime.MinValue,
                StartTime = appointment.Availability?.StartTime ?? TimeSpan.Zero,
                EndTime = appointment.Availability?.EndTime ?? TimeSpan.Zero,
                LocationName = appointment.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = appointment.Payment?.Amount
            };
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(int doctorId)
        {
            var appointments = await repo.GetAppointmentsAsync(doctorId);

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                Status = a.Status,
                Notes = a.Notes,
                PatientName = a.PatientProfile?.User != null
                    ? $"{a.PatientProfile.User.FirstName} {a.PatientProfile.User.LastName}"
                    : string.Empty,
                DoctorName = a.DoctorProfile?.User != null
                    ? $"{a.DoctorProfile.User.FirstName} {a.DoctorProfile.User.LastName}"
                    : string.Empty,
                DoctorId = a.DoctorId,
                Date = a.Availability?.Date ?? DateTime.MinValue,
                StartTime = a.Availability?.StartTime ?? TimeSpan.Zero,
                EndTime = a.Availability?.EndTime ?? TimeSpan.Zero,
                LocationName = a.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = a.Payment?.Amount
            }).ToList();
        }

        public async Task<DoctorProfile?> GetDoctorByUserIdAsync(int userId)
        {
            return await repo.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<DoctorProfileDto>> GetApprovedDoctorsAsync()
        {
            var doctors = await repo.GetApprovedDoctorsAsync();

            return doctors.Select(d => new DoctorProfileDto
            {
                Id = d.Id,
                Price = d.Price,
                Bio = d.Bio,
                ContactNumber = d.ContactNumber,
                IsApproved = d.IsApproved,
                UserId = d.UserId,
                UserName = d.User != null ? $"{d.User.FirstName} {d.User.LastName}" : string.Empty,
                UserEmail = d.User?.Email ?? string.Empty,
                SpecialityId = d.SpecialityId,
                SpecialityName = d.Speciality?.Name ?? string.Empty,
                Locations = d.DoctorLocations?.Select(dl => new LocationDto
                {
                    Id = dl.Location.Id,
                    Name = dl.Location.Name,
                    Address = dl.Location.Address,
                    City = dl.Location.City
                }).ToList() ?? new List<LocationDto>(),
                AverageRating = d.Ratings?.Any() == true ? d.Ratings.Average(r => r.Rate) : 0
            }).ToList();
        }

        public async Task<DoctorProfileDto?> GetDoctorProfileAsync(int id)
        {
            var doctor = await repo.GetByIdAsync(id);
            if (doctor is null) return null;

            return new DoctorProfileDto
            {
                Id = doctor.Id,
                Price = doctor.Price,
                Bio = doctor.Bio,
                ContactNumber = doctor.ContactNumber,
                IsApproved = doctor.IsApproved,
                UserId = doctor.UserId,
                UserName = doctor.User != null ? $"{doctor.User.FirstName} {doctor.User.LastName}" : string.Empty,
                UserEmail = doctor.User?.Email ?? string.Empty,
                SpecialityId = doctor.SpecialityId,
                SpecialityName = doctor.Speciality?.Name ?? string.Empty,
                Locations = doctor.DoctorLocations?.Select(dl => new LocationDto
                {
                    Id = dl.Location.Id,
                    Name = dl.Location.Name,
                    Address = dl.Location.Address,
                    City = dl.Location.City
                }).ToList() ?? new List<LocationDto>(),
                AverageRating = doctor.Ratings?.Any() == true ? doctor.Ratings.Average(r => r.Rate) : 0
            };
        }

        public async Task<IEnumerable<RatingDto>> GetReviewsByDoctorIdAsync(int doctorId)
        {
            var reviews = await repo.GetReviewsByDoctorIdAsync(doctorId);

            return reviews.Select(r => new RatingDto
            {
                Id = r.Id,
                Rate = r.Rate,
                Review = r.Review,
                PatientName = r.PatientProfile?.User != null
                    ? $"{r.PatientProfile.User.FirstName} {r.PatientProfile.User.LastName}"
                    : string.Empty,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<IEnumerable<AvailabilityDto>> GetSlotsByDoctorIdAsync(int doctorId)
        {
            var slots = await repo.GetSlotsByDoctorIdAsync(doctorId);

            return slots.Select(s => new AvailabilityDto
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                IsBooked = s.IsBooked,
                Location = s.Location != null
                    ? new LocationDto
                    {
                        Id = s.Location.Id,
                        Name = s.Location.Name,
                        Address = s.Location.Address,
                        City = s.Location.City
                    }
                    : new LocationDto()
            }).ToList();
        }

        public async Task<bool> RejectDoctorAsync(int id)
        {
            var doctor = await repo.GetByIdAsync(id);
            if (doctor is null) return false;

            doctor.IsApproved = false;
            doctor.UpdatedAt = DateTime.UtcNow;

            repo.Update(doctor);
            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<AppointmentDto?> UpdateAppointmentAsync(int appointmentId, AppointmentStatus status)
        {
            var appointment = await repo.GetAppointmentByIdAsync(appointmentId);
            if (appointment is null) return null;

            appointment.Status = status;
            appointment.UpdatedAt = DateTime.UtcNow;

            await repo.UpdateAppointmentAsync(appointment);
            await repo.SaveChangesAsync();

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
                DoctorId = appointment.DoctorId,
                Date = appointment.Availability?.Date ?? DateTime.MinValue,
                StartTime = appointment.Availability?.StartTime ?? TimeSpan.Zero,
                EndTime = appointment.Availability?.EndTime ?? TimeSpan.Zero,
                LocationName = appointment.Availability?.Location?.Name ?? string.Empty,
                PaymentAmount = appointment.Payment?.Amount
            };
        }

        public async Task<DoctorProfileDto?> UpdateProfileAsync(int id, UpdateDoctorDto dto)
        {
            var doctor = await repo.GetByIdAsync(id);
            if (doctor is null) return null;

            doctor.Price = dto.Price;
            doctor.Bio = dto.Bio;
            doctor.ContactNumber = dto.ContactNumber;
            doctor.SpecialityId = dto.SpecialityId;
            doctor.UpdatedAt = DateTime.UtcNow;

            if (dto.LocationIds != null && dto.LocationIds.Any())
            {
                var existingLocations = doctor.DoctorLocations.ToList();
                foreach (var loc in existingLocations)
                {
                    doctor.DoctorLocations.Remove(loc);
                }

                foreach (var locationId in dto.LocationIds)
                {
                    doctor.DoctorLocations.Add(new DoctorLocation
                    {
                        DoctorId = doctor.Id,
                        LocationId = locationId,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            repo.Update(doctor);
            await repo.SaveChangesAsync();

            var updatedDoctor = await repo.GetByIdAsync(id);
            if (updatedDoctor is null) return null;

            return new DoctorProfileDto
            {
                Id = updatedDoctor.Id,
                Price = updatedDoctor.Price,
                Bio = updatedDoctor.Bio,
                ContactNumber = updatedDoctor.ContactNumber,
                IsApproved = updatedDoctor.IsApproved,
                UserId = updatedDoctor.UserId,
                UserName = updatedDoctor.User != null ? $"{updatedDoctor.User.FirstName} {updatedDoctor.User.LastName}" : string.Empty,
                UserEmail = updatedDoctor.User?.Email ?? string.Empty,
                SpecialityId = updatedDoctor.SpecialityId,
                SpecialityName = updatedDoctor.Speciality?.Name ?? string.Empty,
                Locations = updatedDoctor.DoctorLocations?.Select(dl => new LocationDto
                {
                    Id = dl.Location.Id,
                    Name = dl.Location.Name,
                    Address = dl.Location.Address,
                    City = dl.Location.City
                }).ToList() ?? new List<LocationDto>(),
                AverageRating = updatedDoctor.Ratings?.Any() == true ? updatedDoctor.Ratings.Average(r => r.Rate) : 0
            };
        }
    }
}
