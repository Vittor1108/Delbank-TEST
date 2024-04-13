using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Commons.Interfaces
{
    public interface IResponseCommon
    {
        public Dictionary<string, object> GenerateHttpResponse(string msg, int status, object result);
    }
}
