using LeoClinic.Application.DTOs.Patient;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientProfileDTO> CreatePatientProfileAsync(CreatePatientDTO createPatientDTO)
        {
            var patient = new PatientProfile
            {
                Address = createPatientDTO.Address,
                ContactNumber = createPatientDTO.ContactNumber,
                DateOfBirth = createPatientDTO.DateOfBirth,
                UserId = createPatientDTO.UserId
            };

            // Repository now loads User before returning
            patient = await _patientRepository.CreatePatientAsync(patient);

            var result = new PatientProfileDTO
            {
                Id = patient.Id, 
                UserId = patient.UserId,
                Address = patient.Address,
                ContactNumber = patient.ContactNumber,
                DateOfBirth = patient.DateOfBirth,
                IsApproved = patient.IsApproved,
                FirstName = patient.User?.FirstName ?? string.Empty,
                LastName = patient.User?.LastName ?? string.Empty,
                Email = patient.User?.Email ?? string.Empty,
                Role = patient.User?.Role ?? default,
                IsBlocked = patient.User?.IsBlocked ?? false
            };

            return result;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            try
            {
                await _patientRepository.DeletePatientAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<PatientProfileDTO>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();
            var results = patients.Select(p => new PatientProfileDTO
            {
                Id = p.Id,
                Address = p.Address,
                ContactNumber = p.ContactNumber,
                DateOfBirth = p.DateOfBirth,
                UserId = p.UserId,
                IsApproved = p.IsApproved,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Email = p.User.Email,
                IsBlocked = p.User.IsBlocked,
            }).ToList();
            return results;
        }

        public async Task<List<PatientProfileDTO>> GetApprovedPatientsAsync()
        {
            var patients = await _patientRepository.GetApprovedPatientsAsync();
            var results = patients.Select(p => new PatientProfileDTO
            {
                Id = p.Id,
                Address = p.Address,
                ContactNumber = p.ContactNumber,
                DateOfBirth = p.DateOfBirth,
                UserId = p.UserId,
                IsApproved = p.IsApproved,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Email = p.User.Email,
                IsBlocked = p.User.IsBlocked,
            }).ToList();
            return results;
        }

        public async Task<PatientProfileDTO?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByUserIdAsync(id);
            var result = (patient is null)? null : new PatientProfileDTO{
                Id = patient.Id,
                Address = patient.Address,
                ContactNumber = patient.ContactNumber,
                DateOfBirth = patient.DateOfBirth,
                UserId = patient.UserId,
                IsApproved = patient.IsApproved,
                FirstName = patient.User.FirstName,
                LastName = patient.User.LastName,
                Email = patient.User.Email,
                IsBlocked = patient.User.IsBlocked,
            };
            return result;
        }

        public async Task<bool> UpdatePatientProfileAsync(int id, UpdatePatientProfileDTO patientProfile)
        {
            var patient = await _patientRepository.GetPatientByUserIdAsync(id);
            if (patient != null)
            {
                patient.Address = patientProfile.Address;
                patient.ContactNumber = patientProfile.ContactNumber;
                patient.DateOfBirth = patientProfile.DateOfBirth;
                patient.User.FirstName = patientProfile.FirstName;
                patient.User.LastName = patientProfile.LastName;
                patient.User.Email = patientProfile.Email;
                patient.UpdatedAt = DateTime.UtcNow;
                try
                {
                    await _patientRepository.UpdatePatientProfileAsync(patient);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else { 
                return false;
            }
        }

        public async Task<bool> ApprovePatientAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient != null)
            {
                await _patientRepository.ApprovePatientAsync(id);
                return true;
            }
            else {  return false; }
        }
    }
}
