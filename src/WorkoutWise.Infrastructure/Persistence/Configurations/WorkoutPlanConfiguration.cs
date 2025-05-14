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
    internal class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.HasKey(wp => wp.Id);

            builder.Property(wp => wp.Id)
                .HasConversion(workoutPlan => workoutPlan.Value, value => new WorkoutPlanId(value))
                .ValueGeneratedNever();

            builder.Property(wp => wp.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasMany(wp => wp.Workouts)
                .WithOne()
                .HasForeignKey(w => w.WorkoutPlanId);

            builder.HasIndex(wp => wp.UserId);
        }
    }
}
