using LeoClinic.Domain.Entities;
using System;

namespace LeoClinic.Application.Interfaces
{
    public interface IJwtService
    {
        (string Token, DateTime ExpiresAt) GenerateToken(User user);
    }
}
