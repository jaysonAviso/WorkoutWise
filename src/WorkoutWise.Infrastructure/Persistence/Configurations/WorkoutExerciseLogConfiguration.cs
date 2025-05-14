using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;

namespace WorkoutWise.Infrastructure.Persistence.Configurations;

internal class WorkoutExerciseLogConfiguration : IEntityTypeConfiguration<WorkoutExerciseLog>
{
    public void Configure(EntityTypeBuilder<WorkoutExerciseLog> builder) 
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasConversion(workoutDetail => workoutDetail.Value, value => new WorkoutExerciseLogId(value))
            .ValueGeneratedNever();

        builder.Property(d => d.WorkoutId)
            .HasConversion(workout => workout.Value, value => new WorkoutLogId(value));

        builder.Property(d => d.ExerciseName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(d => d.Notes)
            .HasMaxLength(250);

        builder.HasOne<WorkoutLog>()
            .WithMany()
            .HasForeignKey(workout => workout.WorkoutId);

        builder.Property(d => d.IsDeleted)
            .HasDefaultValue(false);

        builder.HasIndex(d => d.WorkoutId).IsUnique();

    }
}
