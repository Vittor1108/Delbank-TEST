using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Entities.SQL
{
    public class DirectorEntitySQL : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }        
        public virtual IList<DvdEntitySQL>? DVDs { get; set; }
    }
}
