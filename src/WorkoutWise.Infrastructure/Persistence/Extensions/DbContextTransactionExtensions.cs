using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WorkoutWise.Application.Interfaces;
using System.Diagnostics;
using WorkoutWise.Domain.Common.Results;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace WorkoutWise.Infrastructure.Persistence.Extensions
{
    public static class DbContextTransactionExtensions
    {

        public static async Task<ResultT<T>> ExecuteTransactionAsync<T>(
            this ApplicationDbContext dbContext, 
            Func<Task<T>> operation, 
            CancellationToken cancellationToken = default, 
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (dbContext is not ApplicationDbContext concreteContext)
                return ResultT<T>.Failure("ExecuteTransactionAsync requires the concrete ApplicationDbContext.");

            await using var transaction = await concreteContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            try
            {
                var result = await operation();

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return ResultT<T>.Success(result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ResultT<T>.Failure($"Transaction Failed: {ex.Message}");
            }
        }

        public static async Task<ResultT<T>> ExecuteTransactionAsync<T>(
            this ApplicationDbContext dbContext,
            Func<Task<ResultT<T>>> operation,
            CancellationToken cancellationToken = default,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (dbContext is not ApplicationDbContext concreteContext)
                return ResultT<T>.Failure("ExecuteTransactionAsync requires the concrete ApplicationDbContext.");

            await using var transaction = await concreteContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            try
            {
                var result = await operation();

                if (result.IsFailure)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return result;
                }

                await concreteContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ResultT<T>.Failure($"Transaction Failed: {ex.Message}");
            }
        }
    }
}
