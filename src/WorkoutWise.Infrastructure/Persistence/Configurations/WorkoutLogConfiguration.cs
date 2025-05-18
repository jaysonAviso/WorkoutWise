using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;

namespace WorkoutWise.Infrastructure.Persistence.Configurations;

internal class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
{
    public void Configure(EntityTypeBuilder<WorkoutLog> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(w => w.Id)
            .HasConversion(workout => workout.Value, value => new WorkoutLogId(value))
            .ValueGeneratedNever();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            );

        builder.Property(e => e.WorkoutPlanId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new WorkoutPlanId(value)
            );

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(w => w.WorkoutExerciseLogs)
            .WithOne()
            .HasForeignKey(d => d.WorkoutId);

        builder.Property(w => w.DayOfWeek)
            .IsRequired();

        builder.Property(w => w.WorkoutDate)
            .IsRequired();

        builder.HasIndex(w => w.UserId).IsUnique(); 

        builder.HasIndex(w => w.WorkoutPlanId).IsUnique();
    }
}
