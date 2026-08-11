using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.DTOs.Patient
{
    public class UpdatePatientProfileDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Contact number must be an 11-digit phone number starting with 01 (e.g. 01229494438).")]
        public string ContactNumber { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
    }
}
