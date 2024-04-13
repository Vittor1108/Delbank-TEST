using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Messaging.Interfaces
{
    public interface IPublisher
    {
        void SendMessage(string obj, string queue);
    }
}
