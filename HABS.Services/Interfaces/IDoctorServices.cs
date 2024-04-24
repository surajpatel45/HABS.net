using HABS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Services.Interfaces
{
    internal interface IDoctorServices
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<string> AddDoctorAsync(Doctor doctor);
        Task<string> UpdateDoctorAsync(Doctor doctor);
        Task<string> DeleteDoctorAsync(int id);
        Task<List<Doctor>> SearchDoctorBySpecializationAsync(string specialization);
    }
}
