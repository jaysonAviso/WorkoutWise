using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasConversion(w => w.Value, value => new UserId(value))
                .ValueGeneratedNever();

            builder.Property(w => w.FirstName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(w => w.LastName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(w => w.MiddleName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(w => w.Username)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(w => w.HasPublicProfile);
        }
    }
}
