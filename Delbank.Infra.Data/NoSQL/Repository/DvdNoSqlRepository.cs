using Delbank.Domain.Entities.NoSQL;
using Delbank.Domain.Interfaces.Repositories.NoSQL;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Infra.Data.NoSQL.Repository
{
    public class DvdNoSqlRepository : IDvdNoSQLRepository
    {
        private readonly IMongoCollection<DvdNoSqlEntity> _dvdCollection;

        public DvdNoSqlRepository(IOptions<DvdNoSQLEntitySettings> dvdServices)
        {
            MongoClient mongoClient = new MongoClient(dvdServices.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(dvdServices.Value.DatabaseName);

            _dvdCollection = mongoDatabase.GetCollection<DvdNoSqlEntity>(dvdServices.Value.DvdCollectionName);
        }
        
        public async Task CreateDvd(DvdNoSqlEntity dvd)
        {
            await _dvdCollection.InsertOneAsync(dvd);
        }

        public async Task<DvdNoSqlEntity> FindOneDvd(Guid id)
        {
            DvdNoSqlEntity dvd = await _dvdCollection.Find(x => x.Id == id).FirstAsync();
            return dvd;
        }

        public async Task DesactiveDvd(Guid id)
        {
            DvdNoSqlEntity dvd = await FindOneDvd(id);
            dvd.Active = false;
            dvd.DeletedAt = DateTime.Now;

            await _dvdCollection.ReplaceOneAsync(x => x.Id == id, dvd);
        }

        public async Task UpdateDvd(DvdNoSqlEntity dvd, Guid id)
        {
            await _dvdCollection.ReplaceOneAsync(x => x.Id == id, dvd);
        }
    }
}
