using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Application.Interfaces;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;

namespace WorkoutWise.Infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WorkoutLog> Workouts { get; set; }
        public DbSet<WorkoutExerciseLog> WorkoutDetails { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<Workout> workouts { get; set; }
        public DbSet<WorkoutExercise> workoutsExercise { get; set; }
        public DbSet<ExerciseSet> workoutsSet { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<WorkoutExerciseLog>().HasQueryFilter(w => !w.IsDeleted);

        }

        public override int SaveChanges()
        {
            ApplySoftDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplySoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplySoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
