using HABS.Models;
using HABS.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace HABS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentServices _appointmentServices;
        private readonly ConnectionFactory _connectionFactory;

        public AppointmentController(AppointmentServices appointmentServices)
        {
            _appointmentServices = appointmentServices;

            // Initialize RabbitMQ connection factory
            _connectionFactory = new ConnectionFactory
            {
                HostName = "192.168.3.199",
                Port = 5672,
                UserName = "quixy",
                Password = "@quixy123#$",
                VirtualHost = "Bala"
            };
        }

        [HttpGet]
        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _appointmentServices.GetAllAppointmentsAsync();
        }

        [HttpGet]
        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _appointmentServices.GetAppointmentByIdAsync(id);
        }

        [HttpPost]
        public async Task<string> AddAppointment(Appointment appointment)
        {
            try
            {
                var result = await _appointmentServices.AddAppointmentAsync(appointment);
                if (result != null)
                {
                    // Send message to RabbitMQ queue
                    SendMessageToRabbitMQ($"Appointment added: {appointment.Id}");

                    // If needed, you can retrieve the added appointment and further process it
                    //Appointment appointmentAdded = await _appointmentServices.GetAppointmentByIdAsync(appointment.Id);

                    return "Appointment added";
                }
                else
                {
                    return "Appointment not scheduled due to some error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete]
        public async Task<string> DeleteAppointment(int id)
        {
            try
            {
                var result = await _appointmentServices.DeleteAppointmentAsync(id);

                // Send message to RabbitMQ queue
                SendMessageToRabbitMQ($"Appointment deleted: {id}");

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        public async Task<List<Appointment>> SearchAppointmentByDoctorName(string doctorName)
        {
            return await _appointmentServices.SearchAppointmentByDoctorNameAsync(doctorName);
        }

        [HttpGet]
        public async Task<List<Appointment>> SearchAppointmentByDate(DateTime date)
        {
            return await _appointmentServices.SearchAppointmentByDateAsync(date);
        }

        [HttpGet]
        public async Task<List<Appointment>> SearchAppointmentByDisease(string disease)
        {
            return await _appointmentServices.SearchAppointmentByDiseaseAsync(disease);
        }

        [HttpGet]
        public async Task<List<Appointment>> SortAppointmentByDisease()
        {
            return await _appointmentServices.SortAppointmentByDiseaseAsync();
        }

        [HttpGet]
        public async Task<List<Appointment>> SortAppointmentByDate()
        {
            return await _appointmentServices.SortAppointmentByDateAsync();
        }

        // Other actions remain unchanged...

        private void SendMessageToRabbitMQ(string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "SurajPatel",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "SurajPatel",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
