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
    public class ConsumerUpdate : BackgroundService
    {
        private readonly IMessagingConnection _messageingConnection;
        private readonly IModel _channel;
        private IConnection _connection;
        private IDvdNoSQLRepository _dvdNoSQLRepository;

        public ConsumerUpdate(IMessagingConnection messageConnection, IDvdNoSQLRepository dvdNoSqlRepository)
        {
            _messageingConnection = messageConnection;
            _connection = _messageingConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "Update", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _dvdNoSQLRepository = dvdNoSqlRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string jsonBody = Encoding.UTF8.GetString(body);

                DvdNoSqlEntity? dvd = JsonSerializer.Deserialize<DvdNoSqlEntity>(jsonBody);
                
                await _dvdNoSQLRepository.UpdateDvd(dvd, dvd.Id);
                
                await Task.Yield();
            };

            _channel.BasicConsume(queue: "Update", autoAck: true, consumer: consumer);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            base.StopAsync(cancellationToken);
        }
    }
}
