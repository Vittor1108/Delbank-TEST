using Delbank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Interfaces.Repositories.SQL
{
    public  interface IBaseRepositorySQL<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task<IList<TEntity>> Select();
        Task<TEntity> Select(Guid id);
        Task Desactive(Guid id);
    }
}
