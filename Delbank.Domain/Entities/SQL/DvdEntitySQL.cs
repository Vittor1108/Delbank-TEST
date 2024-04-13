using Delbank.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Entities.SQL
{
    public class DvdEntitySQL : BaseEntity
    {
        public string? Title { get; set; }
        public EGenre Genre { get;set; }
        public DateTime Published { get; set; }
        public int Copies { get; set; }
        public bool Avaliable { get; set; } 
        public Guid FkDirector { get; set; }
        public DirectorEntitySQL? Director { get; set; }
    }
}
