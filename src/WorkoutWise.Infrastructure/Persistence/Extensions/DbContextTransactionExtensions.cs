using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WorkoutWise.Application.Interfaces;
using System.Diagnostics;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Infrastructure.Persistence.Extensions
{
    public static class DbContextTransactionExtensions
    {

        public static async Task<Result> ExecuteTransactionAsync(this ApplicationDbContext dbContext, Func<Task> operation, CancellationToken cancellationToken = default)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await operation();
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure($"Transaction Failed: {ex.Message}");
            }
        }
    }
}
