using Delbank.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Commons
{
    public class ResponseCommon : IResponseCommon
    {
        public Dictionary<string, object> GenerateHttpResponse(string msg, int status, object result)
        {
            Dictionary<string, object> response = new Dictionary<string, object>
            {
                {"msg", msg},
                {"status", status},
                {"result", result}
            };
            return response;
        }        
    }
}
