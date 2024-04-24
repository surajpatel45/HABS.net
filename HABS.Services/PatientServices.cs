using HABS.Data;
using HABS.Models;
using HABS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Services
{

    public class PatientServices : IPatientServices
    {
        private readonly MyDbContext _context;
        public PatientServices(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<string> AddPatientAsync(Patient patient)
        {
            try
            {
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return "Patient added successfully.";
            }
            catch (Exception ex)
            {
                return "Failed to add patient: " + ex.Message;
            }
        }

        public async Task<string> UpdatePatientAsync(Patient patient)
        {
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == patient.Id);
            if (existingPatient == null)
            {
                return "Patient not found";
            }
            existingPatient.Name = patient.Name;
            existingPatient.Email = patient.Email;
            existingPatient.Disease = patient.Disease;
            await _context.SaveChangesAsync();

            return "Patient updated";
        }

        public async Task<string> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return "Patient deleted";
            }
            return "Patient not found";
        }

        
    }
}
