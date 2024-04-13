using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Messaging.Interfaces
{
    public interface IMessagingConnection
    {
        IConnection CreateConnection();
    }
}
