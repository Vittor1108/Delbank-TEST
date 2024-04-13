using Delbank.Domain.Entities.SQL;
using Delbank.Infra.Data.SQL.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Infra.Data.SQL.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<DirectorEntitySQL> DirectorEntity { get; set; }
        public DbSet<DvdEntitySQL> DvdEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DirectorEntitySQL>(new DirectorMap().Configure);
            modelBuilder.Entity<DvdEntitySQL>(new DvdMap().Configure);
        }

    }
}
