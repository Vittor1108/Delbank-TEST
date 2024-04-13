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
    public class ConsumerInsertData : BackgroundService
    {
        private readonly IMessagingConnection _messageingConnection;
        private readonly IModel _channel;
        private IConnection _connection;
        private IDvdNoSQLRepository _dvdNoSQLRepository;

        public ConsumerInsertData(IMessagingConnection messageConnection, IDvdNoSQLRepository dvdNoSqlRepository)
        {
            _messageingConnection = messageConnection;
            _connection = _messageingConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "Insert", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _dvdNoSQLRepository = dvdNoSqlRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var jsonBody = Encoding.UTF8.GetString(body);

                DvdNoSqlEntity? dvdEntity = JsonSerializer.Deserialize<DvdNoSqlEntity>(jsonBody);

                if (dvdEntity != null)
                {
                    await _dvdNoSQLRepository.CreateDvd(dvdEntity);
                }

                await Task.Yield();
            };

            _channel.BasicConsume(queue: "Insert", autoAck: true, consumer: consumer);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            base.StopAsync(cancellationToken);
        }
    }
}
