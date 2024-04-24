using HABS.Data;
using HABS.Models;
using HABS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HABS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private DoctorServices _doctorServices;
        public DoctorController(DoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }

        [HttpGet]
        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _doctorServices.GetAllDoctorsAsync();
        }

        [HttpGet]
        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _doctorServices.GetDoctorByIdAsync(id);
        }

        [HttpPost]
        public async Task<string> AddDoctor(Doctor doctor)
        {
            return await _doctorServices.AddDoctorAsync(doctor);
        }

        [HttpPut]
        public async Task<string> UpdateDoctor(Doctor doctor)
        {
            return await _doctorServices.UpdateDoctorAsync(doctor);
        }

        [HttpDelete]
        public async Task<string> DeleteDoctor(int id)
        {
            return await _doctorServices.DeleteDoctorAsync(id);
        }

        [HttpGet]
        public async Task<List<Doctor>> SearchDoctorBySpecialization(string specialization)
        {
            return await _doctorServices.SearchDoctorBySpecializationAsync(specialization);
        }
    }
}
