using RabbitMQ.Client;

namespace HABS.API
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;

        public RabbitMQService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "192.168.3.199", 
                Port = 5672,
                UserName = "quixy",
                Password = "@quixy123#$",
                VirtualHost = "Bala"
            };

            _connection = _connectionFactory.CreateConnection();
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public void DeclareExchangeAndBindQueue(string exchangeName, string queueName, string routingKey)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
            }
        }
    }
}
