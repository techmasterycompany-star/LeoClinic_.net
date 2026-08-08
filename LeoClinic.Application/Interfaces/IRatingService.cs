using LeoClinic.Application.DTOs.Doctor;
using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Interfaces
{
    public interface IRatingService
    {
        Task<RatingDto?> GetRatingByIdAsync(int id);
        Task<Rating> CreateRating(CreateRatingDTO rating);
        Task<bool> UpdateRating(int id, UpdateRatingDTO rating);
        Task<bool> DeleteRating(int id);
    }
}
