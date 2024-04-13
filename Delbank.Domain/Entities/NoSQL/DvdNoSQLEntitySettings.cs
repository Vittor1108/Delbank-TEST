using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Entities.NoSQL
{
    public class DvdNoSQLEntitySettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set;}
        public string? DvdCollectionName { get; set; }
    }
}
