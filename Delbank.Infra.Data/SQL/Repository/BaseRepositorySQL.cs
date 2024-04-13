using Delbank.Domain.Entities;
using Delbank.Domain.Interfaces.Repositories.SQL;
using Delbank.Infra.Data.SQL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Infra.Data.SQL.Repository
{
    public class BaseRepositorySQL<TEntity> : IBaseRepositorySQL<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlContext _dbContext;
        public BaseRepositorySQL(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Guid id)
        {
            TEntity entity = await Select(id);

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Insert(TEntity entity)
        {
            Guid guid = Guid.NewGuid();
            entity.Id = guid;
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Select()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Select(Guid id)
        {
            return await _dbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
        }

        public async Task Update(TEntity entity)
        {            
            entity.UpdatedAt = DateTime.Now;
            _dbContext.Set<TEntity>().Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Desactive(Guid id)
        {
            TEntity entity = await Select(id);

            entity.DeletedAt = DateTime.Now;
            entity.Active = false;
            await _dbContext.SaveChangesAsync();
        }
    }
}
