using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<PatientProfile>> GetAllPatientsAsync();
        Task<List<PatientProfile>> GetApprovedPatientsAsync();
        Task<PatientProfile?> GetPatientByIdAsync(int id);
        Task<int> GetPatientIdByUserIdAsync(int userId);
        Task<PatientProfile> CreatePatientAsync(PatientProfile patientProfile);
        Task<PatientProfile?> GetPatientByUserIdAsync(int userId);
        Task UpdatePatientProfileAsync(PatientProfile patientProfile);
        Task DeletePatientAsync(int id);
        Task ApprovePatientAsync(int id);
    }
}
