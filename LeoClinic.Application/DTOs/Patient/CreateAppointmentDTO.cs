using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.DTOs.Patient
{
    public class CreateAppointmentDTO
    {
        public int DoctorId { get; set; }
        public int AvailabilityId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
