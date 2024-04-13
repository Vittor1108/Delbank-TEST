using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Entities.NoSQL
{
    public class DvdNoSqlEntity : BaseEntity
    {
        [BsonElement("Id")]
        public override Guid Id { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Genre")]
        public EGenre Genre { get; set; }

        [BsonElement("Published")]
        public DateTime Published { get; set; }

        [BsonElement("Copies")]
        public int Copies { get; set; }

        [BsonElement("Avaliable")]
        public bool Avaliable { get; set; }

        //public Guid FkDirector { get; set; }
        //public DirectorEntitySQL? Director { get; set; }
    }
}
