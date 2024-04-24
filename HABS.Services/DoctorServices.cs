using HABS.Data;
using HABS.Models;
using HABS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HABS.Services
{

    public class DoctorServices : IDoctorServices
    {
        private readonly MyDbContext _context;
        public DoctorServices(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<string> AddDoctorAsync(Doctor doctor)
        {
            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return "Doctor added successfully.";
            }
            catch (Exception ex)
            {
                return "Failed to add doctor: " + ex.Message;
            }
        }

        public async Task<string> UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _context.Doctors
                .FirstOrDefaultAsync(p => p.Id == doctor.Id);
            if (existingDoctor == null)
            {
                return "Doctor not found";
            }
            existingDoctor.Name = doctor.Name;
            existingDoctor.Specialization = doctor.Specialization;
            await _context.SaveChangesAsync();

            return "Doctor updated";
        }

        public async Task<string> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return "Doctor deleted";
            }
            return "Doctor not found";
        }

        public async Task<List<Doctor>> SearchDoctorBySpecializationAsync(string specialization)
        {
            var doctors = await _context.Doctors
                .Where(d => d.Specialization == specialization)
                .ToListAsync();
            return doctors;
        }
    }
}
