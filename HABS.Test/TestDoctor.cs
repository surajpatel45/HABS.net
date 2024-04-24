using HABS.API.Controllers;
using HABS.Data;
using HABS.Models;
using HABS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace HABS.Test
{
    public class TestDoctor
    {
        private DoctorController doctorController;
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql("Server=192.168.5.150;Port=5432;User Id=postgres;Password=n@v@yUg@kw!x##;Database=SurajPatel;Pooling=true;");
            });
            /*services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(connectionString: "server=VMLAP255; database=SurajHospital; Integrated Security=true; MultipleActiveResultSets=true; TrustServerCertificate=True;");
            });*/
            services.AddScoped<DoctorServices>();

            var serviceProvider = services.BuildServiceProvider();
            var doctorService = serviceProvider.GetRequiredService<DoctorServices>();

            doctorController = new DoctorController(doctorService);
        }

        [Test]
        public async Task TestGetAllDoctors()
        {
            var result = await doctorController.GetAllDoctors();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task TestAddDoctor()
        {
            Doctor doctor = new Doctor { Id = 4, Name = "Mohan", Specialization = "Teeth" };
            var result = await doctorController.AddDoctor(doctor);
            Assert.That(result, Is.EqualTo("Doctor added successfully."));
        }

        [Test]
        public async Task TestGetDoctorById()
        {
            var result = await doctorController.GetDoctorById(4);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task TestUpdateDoctor()
        {
            Doctor doctor = new Doctor { Id = 4, Name = "Mohan", Specialization = "Eye" };
            var result = await doctorController.UpdateDoctor(doctor);
            Assert.That(result, Is.EqualTo("Doctor updated"));
        }

        [Test]
        public async Task TestDeleteDoctor()
        {
            var result = await doctorController.DeleteDoctor(4);
            Assert.That(result, Is.EqualTo("Doctor deleted"));
        }
        
    }
}