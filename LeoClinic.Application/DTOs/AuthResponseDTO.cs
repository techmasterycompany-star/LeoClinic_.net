using LeoClinic.Domain.Enums;
using System;

namespace LeoClinic.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAt { get; set; }
        public DateTime TokenExpiresAt { get; set; }
    }
}
