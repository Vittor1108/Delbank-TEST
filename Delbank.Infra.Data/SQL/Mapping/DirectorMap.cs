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
    public class DirectorMap : IEntityTypeConfiguration<DirectorEntitySQL>
    {
        public void Configure(EntityTypeBuilder<DirectorEntitySQL> builder)
        {
            builder.ToTable("Director");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(x => x.Surname)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(x => x.Active)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.UpdatedAt)
                   .IsRequired(false);

            builder.Property(x => x.DeletedAt)
                   .IsRequired(false);

            builder.HasMany(x => x.DVDs)
                   .WithOne(x => x.Director)
                   .HasForeignKey(x => x.FkDirector);
        }
    }
}
