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
    public class AvailabilitytRepository : IAvailabilityRepository
    {
        private readonly AppDbContext _dbContext;
        public AvailabilitytRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Availability?> GetSlotByIdAsync(int id)
        {
            return await _dbContext.Availabilities.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
