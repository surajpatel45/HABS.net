using HABS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Services.Interfaces
{
    internal interface IPatientServices
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<string> AddPatientAsync(Patient patient);
        Task<string> UpdatePatientAsync(Patient patient);
        Task<string> DeletePatientAsync(int id);
    }
}
