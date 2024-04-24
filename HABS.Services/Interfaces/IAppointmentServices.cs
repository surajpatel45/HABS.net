using HABS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Services.Interfaces
{
    internal interface IAppointmentServices
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<string> AddAppointmentAsync(Appointment appointment);
        Task<string> DeleteAppointmentAsync(int id);
        Task<List<Appointment>> SearchAppointmentByDoctorNameAsync(string doctorName);
        Task<List<Appointment>> SearchAppointmentByDateAsync(DateTime date);
        Task<List<Appointment>> SearchAppointmentByDiseaseAsync(string disease);
        Task<List<Appointment>> SortAppointmentByDiseaseAsync();
        Task<List<Appointment>> SortAppointmentByDateAsync();

    }
}
