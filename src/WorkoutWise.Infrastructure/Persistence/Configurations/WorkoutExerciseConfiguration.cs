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
    internal class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder.HasKey(we => we.Id);

            builder.Property(we => we.Id)
                .HasConversion(we => we.Value, value => new WorkoutExerciseId(value))
                .ValueGeneratedNever();

            builder.Property(we => we.Name)
                .IsRequired();

            builder.Property(we => we.Notes)
                .IsRequired();

            builder.HasMany(we => we.Sets)
                .WithOne()
                .HasForeignKey(we => we.WorkoutExerciseId);

            builder.HasIndex(we => we.WorkoutId);
        }
    }
}
