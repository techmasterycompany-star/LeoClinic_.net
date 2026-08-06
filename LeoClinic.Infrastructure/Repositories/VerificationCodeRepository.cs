using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LeoClinic.Infrastructure.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly AppDbContext _context;

        public VerificationCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VerificationCode verificationCode)
        {
            await _context.VerificationCodes.AddAsync(verificationCode);
        }

        public async Task<VerificationCode?> GetLatestCodeAsync(int userId, string token, VerificationType type)
        {
            return await _context.VerificationCodes
                .Where(v => v.UserId == userId && v.Token == token && v.Type == type)
                .OrderByDescending(v => v.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
