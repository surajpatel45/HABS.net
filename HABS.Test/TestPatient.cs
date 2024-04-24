using HABS.API.Controllers;
using HABS.Data;
using HABS.Models;
using HABS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Test
{
    internal class TestPatient
    {
        private PatientController patientController;
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql("Server=192.168.5.150;Port=5432;User Id=postgres;Password=n@v@yUg@kw!x##;Database=SurajPatel;Pooling=true;");
            });
            services.AddScoped<PatientServices>();

            var serviceProvider = services.BuildServiceProvider();
            var patientService = serviceProvider.GetRequiredService<PatientServices>();

            patientController = new PatientController(patientService);
        }

        [Test]
        public async Task TestGetAllPatients()
        {
            var result = await patientController.GetAllPatients();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task TestAddPatient()
        {
            Patient patient = new Patient { Id = 5, Name = "Ankur", Email = "ankur@gmail.com", Disease = "Eye" };
            var result = await patientController.AddPatient(patient);
            Assert.That(result, Is.EqualTo("Patient added successfully."));
        }

        [Test]
        public async Task TestGetPatientById()
        {
            var result = await patientController.GetPatientById(5);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task TestUpdatePatient()
        {
            Patient patient = new Patient { Id = 5, Name = "Ankur", Email = "ankur@gmail.com", Disease = "Eye sight" };
            var result = await patientController.UpdatePatient(patient);
            Assert.That(result, Is.EqualTo("Patient updated"));
        }

        [Test]
        public async Task TestDeletePatient()
        {
            var result = await patientController.DeletePatient(5);
            Assert.That(result, Is.EqualTo("Patient deleted"));
        }
    }
}
