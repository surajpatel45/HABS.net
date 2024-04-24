using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace HABS.API
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly string _exchangeName = "appointments_exchange"; 
        private readonly string _queueName = "SurajPatel"; 
        private readonly string _routingKey = "appointment_event"; 

        public RabbitMQConsumer()
        {
            _rabbitMQService = new RabbitMQService();
            _rabbitMQService.DeclareExchangeAndBindQueue(_exchangeName, _queueName, _routingKey);
        }

        public void ConsumeMessages()
        {
            try
            {
                using (var channel = _rabbitMQService.CreateChannel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Received message: {message}");
                    };
                    channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
                    Console.WriteLine("Consumer started. Listening for messages...");
                    Console.ReadLine(); // Keep the application running
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consuming messages from RabbitMQ: {ex.Message}");
                throw;
            }
        }
    }
}
