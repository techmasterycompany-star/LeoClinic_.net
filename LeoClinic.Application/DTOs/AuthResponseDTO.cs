using LeoClinic.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LeoClinic.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
