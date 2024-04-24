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

    public class AppointmentServices : IAppointmentServices
    {
        private readonly MyDbContext _context;
        public AppointmentServices(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.
                Include(Appointment => Appointment.Patient).
                Include(Appointment => Appointment.Doctor).
                ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<string> AddAppointmentAsync(Appointment appointment)
        {
            try
            {
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return "Appointment added successfully.";
            }
            catch (Exception ex)
            {
                return "Failed to add appointment: " + ex.Message;
            }
        }

        public async Task<string> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return "Appointment deleted";
            }
            return "Appointment not found";
        }

        public async Task<List<Appointment>> SearchAppointmentByDoctorNameAsync(string doctorName)
        {
            var appointments = await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .Where(a => a.Doctor.Name == doctorName)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> SearchAppointmentByDateAsync(DateTime date)
        {
            var appointments = await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> SearchAppointmentByDiseaseAsync(string disease)
        {
            var appointments = await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .Where(a => a.Patient.Disease == disease)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> SortAppointmentByDiseaseAsync()
        {
            var appointments = await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .OrderBy(a => a.Patient.Disease)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> SortAppointmentByDateAsync()
        {
            var appointments = await _context.Appointments
                .Include(Appointment => Appointment.Patient)
                .Include(Appointment => Appointment.Doctor)
                .OrderBy(a => a.Date)
                .ToListAsync();
            return appointments;
        }
    }
}
