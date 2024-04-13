using Delbank.Domain.Entities.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Interfaces.Repositories.NoSQL
{
    public interface IDvdNoSQLRepository
    {
        Task CreateDvd(DvdNoSqlEntity dvd);
        Task<DvdNoSqlEntity> FindOneDvd(Guid id);
        Task DesactiveDvd(Guid id);
        Task UpdateDvd(DvdNoSqlEntity dvd, Guid id);
    }
}
