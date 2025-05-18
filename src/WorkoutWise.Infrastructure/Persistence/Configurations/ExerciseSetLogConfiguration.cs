using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutLogs.Entities;

namespace WorkoutWise.Infrastructure.Persistence.Configurations
{
    internal class ExerciseSetLogConfiguration : IEntityTypeConfiguration<ExerciseSetLog>
    {
        public void Configure(EntityTypeBuilder<ExerciseSetLog> builder)
        {
            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ExerciseSetLogId(value)
                );

            builder.Property(e => e.SetNumber).IsRequired();
            builder.Property(e => e.Reps).IsRequired();
            builder.Property(e => e.Weight).IsRequired();

            builder.Property(e => e.Unit)
            .IsRequired()
            .HasConversion<int>();

            builder.Property(e => e.WorkoutExerciseLogId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new WorkoutExerciseLogId(value));
        }
    }
}
