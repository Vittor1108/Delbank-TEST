using Delbank.Messaging.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Delbank.Messaging.Publishers
{
    public class Publisher : IPublisher
    {
        private readonly IMessagingConnection _messagingConnection;
        public Publisher(IMessagingConnection messagingConnection)
        {
            _messagingConnection = messagingConnection; 
        }

        public void SendMessage(string obj, string queue) 
        { 
            var connection = _messagingConnection.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            
            byte[] bodyMessage = Encoding.UTF8.GetBytes(obj);

            channel.BasicPublish(exchange: string.Empty, routingKey:queue, basicProperties: null, body: bodyMessage);
        }

    }
}
