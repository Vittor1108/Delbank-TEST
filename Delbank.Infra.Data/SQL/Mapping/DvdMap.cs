using Delbank.Domain.Entities.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Infra.Data.SQL.Mapping
{
    public class DvdMap : IEntityTypeConfiguration<DvdEntitySQL>
    {
        public void Configure(EntityTypeBuilder<DvdEntitySQL> builder)
        {
            builder.ToTable("DVD");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Title)
                    .IsRequired()
                   .HasMaxLength(120);

            builder.Property(x => x.Genre)
                   .IsRequired();

            builder.Property(x => x.Published)
                   .IsRequired();

            builder.Property(x => x.Copies)
                   .IsRequired();

            builder.Property(x => x.Avaliable)
                   .IsRequired();

            builder.Property(x => x.Active).HasDefaultValue(true);
            
            builder.Property(x => x.CreatedAt)
                  .IsRequired()
                  .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.UpdatedAt)
                   .IsRequired(false);

            builder.Property(x => x.DeletedAt)
                   .IsRequired(false);
        }
    }
}
