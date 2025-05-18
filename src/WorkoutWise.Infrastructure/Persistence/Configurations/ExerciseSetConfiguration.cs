using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Infrastructure.Persistence.Configurations
{
    internal class ExerciseSetConfiguration : IEntityTypeConfiguration<ExerciseSet>
    {
        public void Configure(EntityTypeBuilder<ExerciseSet> builder)
        {
            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,              
                    value => new ExerciseSetId(value)
                );

            builder.Property(e => e.SetNumber).IsRequired();
            builder.Property(e => e.Reps).IsRequired();
            builder.Property(e => e.Weight).IsRequired();

            builder.Property(e => e.Unit)
            .IsRequired()
            .HasConversion<int>();

            builder.Property(e => e.WorkoutExerciseId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new WorkoutExerciseId(value)
            );
        }
    }
}
