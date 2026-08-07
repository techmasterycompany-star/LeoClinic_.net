using LeoClinic.Domain.Entities;
using System;

namespace LeoClinic.Application.Interfaces
{
    public interface IJwtService
    {
        (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);
        (string Token, DateTime ExpiresAt) GenerateRefreshToken();
    }
}
