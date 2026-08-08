using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;
        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Rating> CreateRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                throw new Exception($"Rating with id {id} not found.");
            }
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            return await _context.Ratings
                .Include(r => r.PatientProfile).ThenInclude(p => p.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateRating(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}
