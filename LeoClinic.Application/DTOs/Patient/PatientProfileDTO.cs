using LeoClinic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.DTOs.Patient
{
    public class PatientProfileDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Patient;
        public bool IsBlocked { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    }
}
