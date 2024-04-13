using Delbank.Domain.Entities.NoSQL;
using Delbank.Domain.Interfaces.Repositories.NoSQL;
using Delbank.Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Delbank.Messaging.Consumers
{
    public class ConsumerDeletedData : BackgroundService
    {
        private readonly IMessagingConnection _messageingConnection;
        private readonly IModel _channel;
        private IConnection _connection;
        private IDvdNoSQLRepository _dvdNoSQLRepository;

        public ConsumerDeletedData(IMessagingConnection messageConnection, IDvdNoSQLRepository dvdNoSqlRepository)
        {
            _messageingConnection = messageConnection;
            _connection = _messageingConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "Delete", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _dvdNoSQLRepository = dvdNoSqlRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string msg = Encoding.UTF8.GetString(body);

                Guid id = JsonSerializer.Deserialize<Guid>(msg);

                await _dvdNoSQLRepository.DesactiveDvd(id);

                await Task.Yield();
            };

            _channel.BasicConsume(queue: "Delete", autoAck: true, consumer: consumer);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            base.StopAsync(cancellationToken);
        }
    }
}
