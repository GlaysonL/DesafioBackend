using System.Text;
using System.Text.Json;
using DesafioBackend.Model;
using RabbitMQ.Client;

namespace DesafioBackend.Messaging
{
    public class MotorcycleRegisteredPublisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "moto_cadastrada";

        public void Publish(Motorcycle motorcycleEvent)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = JsonSerializer.Serialize(motorcycleEvent);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body
            );
        }
    }
}
