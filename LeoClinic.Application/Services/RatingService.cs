using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        public RatingService(IRatingRepository ratingRepository, IAppointmentRepository appointmentRepository)
        {
            _ratingRepository = ratingRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Rating> CreateRating(int patientId, CreateRatingDTO rating)
        {
            var canReview = await _appointmentRepository.hasCompletedAppointment(patientId, rating.DoctorId);
            if(!canReview)
            {
                throw new InvalidOperationException("Patient has not completed an appointment with this doctor.");
            }

            var ratingEntity = new Rating
            {
                DoctorId = rating.DoctorId,
                PatientId = patientId,
                Rate = rating.RatingValue,
                Review = rating.Review ?? string.Empty
            };

            return await _ratingRepository.CreateRating(ratingEntity);
        }

        public async Task<bool> DeleteRating(int id, int patientId)
        {
            try
            {
                var ratingToDelete = await _ratingRepository.GetRatingByIdAsync(id);
                if (ratingToDelete == null)
                {
                    return false;
                }
                if (ratingToDelete.PatientId != patientId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to update this rating.");
                }
                await _ratingRepository.DeleteRating(ratingToDelete);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<RatingDto?> GetRatingByIdAsync(int id)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(id);
            if (rating == null)
            {
                return null;
            }

            return new RatingDto
            {
                Id = rating.Id,
                Rate = rating.Rate,
                Review = rating.Review,
                PatientName = rating.PatientProfile?.User != null
                    ? $"{rating.PatientProfile.User.FirstName} {rating.PatientProfile.User.LastName}"
                    : string.Empty,
                CreatedAt = rating.CreatedAt
            };
        }


        public async Task<bool> UpdateRating(int id, int patientId, UpdateRatingDTO rating)
        {
            var ratingToUpdate = await _ratingRepository.GetRatingByIdAsync(id);
            if (ratingToUpdate == null)
            {
                return false;
            }
            if (ratingToUpdate.PatientId != patientId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this rating.");
            }
            ratingToUpdate.Rate = rating.RatingValue;
            ratingToUpdate.Review = rating.Review ?? string.Empty;

            await _ratingRepository.UpdateRating(ratingToUpdate);
            return true;
        }
    }
}