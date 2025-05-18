using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;

namespace WorkoutWise.Infrastructure.Persistence.Configurations
{
    internal class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasConversion(w => w.Value, value => new WorkoutId(value))
                .ValueGeneratedNever();

            builder.Property(e => e.WorkoutPlanId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new WorkoutPlanId(value)
            );

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasMany(w => w.Excercises)
                .WithOne()
                .HasForeignKey(w => w.WorkoutId);

            builder.HasIndex(w => w.WorkoutPlanId);
        }
    }
}
