using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;

namespace WorkoutWise.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        DbSet<Workout> workouts { get; set; }
        DbSet<WorkoutExercise> workoutsExercise { get; set; }
        DbSet<ExerciseSet> workoutsSet { get; set; }
        DbSet<WorkoutLog> Workouts { get; set; }
        DbSet<WorkoutExerciseLog> WorkoutDetails { get; set; }
        DbSet<User> users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
