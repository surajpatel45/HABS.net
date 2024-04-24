using RabbitMQ.Client;
using System.Text;

namespace HABS.API
{
    public class RabbitMQProducer
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly string _exchangeName = "Suraj exchange"; 
        private readonly string _routingKey = "Suraj event"; 
        public RabbitMQProducer()
        {
            _rabbitMQService = new RabbitMQService();
            _rabbitMQService.DeclareExchangeAndBindQueue(_exchangeName, "SurajPatel", _routingKey);
        }

        public void SendMessage(string message)
        {
            try
            {
                using (var channel = _rabbitMQService.CreateChannel())
                {
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: _exchangeName, routingKey: _routingKey, basicProperties: null, body: body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to RabbitMQ: {ex.Message}");
                throw;
            }
        }
    }
}
