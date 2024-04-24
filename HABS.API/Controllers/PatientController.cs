using HABS.Models;
using HABS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HABS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private PatientServices _patientServices;
        public PatientController(PatientServices patientServices)
        {
            _patientServices = patientServices;
        }

        [HttpGet]
        public async Task<List<Patient>> GetAllPatients()
        {
            return await _patientServices.GetAllPatientsAsync();
        }

        [HttpGet]
        public async Task<Patient> GetPatientById(int id)
        {
            return await _patientServices.GetPatientByIdAsync(id);
        }

        [HttpPost]
        public async Task<string>  AddPatient(Patient patient)
        {
            return await _patientServices.AddPatientAsync(patient);
        }

        [HttpPut]
        public async Task<string> UpdatePatient(Patient patient)
        {
            return await _patientServices.UpdatePatientAsync(patient);
        }

        [HttpDelete]
        public async Task<string> DeletePatient(int id)
        {
            return await (_patientServices.DeletePatientAsync(id));
        }

    }
}
