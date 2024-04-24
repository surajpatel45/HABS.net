using HABS.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly RabbitMQConsumer _rabbitMQConsumer;

        public ConsumerController(RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
        }

        [HttpGet]
        public IActionResult ProcessAppointments()
        {
            // Start the RabbitMQ consumer to listen for messages
            _rabbitMQConsumer.ConsumeMessages();

            return Ok("RabbitMQ Consumer started processing appointments.");
        }
    }
}
