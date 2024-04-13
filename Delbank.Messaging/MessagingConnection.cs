using Delbank.Messaging.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Messaging
{
    public class MessagingConnection : IMessagingConnection
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public MessagingConnection(string hostName)
        {
            _connectionFactory = new ConnectionFactory() { HostName =  hostName };  
        }


        public IConnection CreateConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
            }
            return _connection;
        }
    }
}
