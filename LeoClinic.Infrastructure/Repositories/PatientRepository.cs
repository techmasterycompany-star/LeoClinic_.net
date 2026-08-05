using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoClinic.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<PatientProfile>> GetAllPatientsAsync()
        {
            return await _context.PatientProfiles.Include(p => p.User).ToListAsync();
        }

        public async Task<List<PatientProfile>> GetApprovedPatientsAsync()
        {
            return await _context.PatientProfiles.Where(p => p.IsApproved).Include(p => p.User).ToListAsync();
        }

        public async Task<PatientProfile?> GetPatientByIdAsync(int id)
        {
            return await _context.PatientProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PatientProfile> CreatePatientAsync(PatientProfile patientProfile)
        {
            var patient = _context.PatientProfiles.Add(patientProfile);
            await _context.SaveChangesAsync();
            return patient.Entity;
        }

        public async Task UpdatePatientProfileAsync(PatientProfile patientProfile)
        {
            _context.PatientProfiles.Update(patientProfile);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePatientAsync(int id)
        {
            var patientProfile = _context.PatientProfiles.Find(id);
            if (patientProfile != null)
            {
                _context.PatientProfiles.Remove(patientProfile);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"patient with id: {id} Not Found");
            }
        }

        public async Task ApprovePatientAsync(int id)
        {
            var patient = await _context.PatientProfiles.FindAsync(id);
            if (patient != null)
            {
                patient.IsApproved = true;
            }
        }
    }
}
