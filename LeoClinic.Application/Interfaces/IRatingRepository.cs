using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating?> GetRatingByIdAsync(int id);
        Task<Rating> CreateRating(Rating rating);
        Task UpdateRating(Rating rating);
        Task DeleteRating(int id);

    }
}
