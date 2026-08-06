using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Interfaces
{
    public interface IVerificationCodeRepository
    {
        Task AddAsync(VerificationCode verificationCode);
        Task<VerificationCode?> GetLatestCodeAsync(int userId, string token, VerificationType type);
        Task SaveChangesAsync();
    }
}
