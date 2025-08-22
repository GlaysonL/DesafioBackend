using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DesafioBackend.Messaging
{
    public class MotorcycleRegisteredConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "moto_cadastrada";
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public MotorcycleRegisteredConsumer(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                using var dbContext = _dbContextFactory.CreateDbContext();
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motoEvent = JsonSerializer.Deserialize<Motorcycle>(message);
                if (motoEvent != null && motoEvent.Year == 2024)
                {
                    var notification = new MotorcycleNotification
                    {
                        Id = motoEvent.Id,
                        Identifier = motoEvent.Identifier,
                        Model = motoEvent.Model,
                        Plate = motoEvent.Plate,
                        Year = motoEvent.Year,
                        ReceivedAt = DateTime.UtcNow
                    };
                    dbContext.MotorcycleNotifications.Add(notification);
                    dbContext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }
    }
}