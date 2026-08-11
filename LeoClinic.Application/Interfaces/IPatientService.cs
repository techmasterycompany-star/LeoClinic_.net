using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientProfileDTO>> GetAllPatientsAsync();
        Task<List<PatientProfileDTO>> GetApprovedPatientsAsync();
        Task<PatientProfileDTO> CreatePatientProfileAsync(CreatePatientDTO createPatientDTO);
        Task<PatientProfileDTO?> GetPatientByIdAsync(int id);
        Task<bool> UpdatePatientProfileAsync(int id, UpdatePatientProfileDTO patientProfile);
        Task<bool> DeletePatientAsync(int id);
        Task<bool> ApprovePatientAsync(int id);
        Task<int> GetPatientIdByUserIdAsync(int id);
    }
}
