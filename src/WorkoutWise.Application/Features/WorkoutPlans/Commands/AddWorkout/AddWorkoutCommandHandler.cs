using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.WorkoutPlans.Commands.AddWorkout
{
    internal sealed class AddWorkoutCommandHandler : IRequestHandler<AddWorkoutCommand, ResultT<WorkoutId>>
    {
        private readonly IApplicationDbContext _context;

        public AddWorkoutCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultT<WorkoutId>> Handle(AddWorkoutCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.BeginTransactionAsync(cancellationToken);

            try
            {
                var plan = await _context.WorkoutPlans
                    .FirstOrDefaultAsync(wp => wp.Id == request.WorkoutPlanId, cancellationToken);

                if (plan is null)
                    return ResultT<WorkoutId>.Failure("Workout plan not found.");

                var workout = WorkoutPlan.CreateWorkout(
                    request.Name,
                    plan.Id!,
                    request.DayOfWeek);

                if (workout.IsFailure)
                    return ResultT<WorkoutId>.Failure(workout.ErrorMesage!);

                var workoutId = plan.AddWorkout(workout.Value!);

                if (workoutId.IsFailure)
                    return ResultT<WorkoutId>.Failure(workoutId.ToString()!);

                request.WorkoutExercisesDto.ForEach(we => {

                    var exercise = WorkoutPlan.CreateWorkoutExercise(
                                we.Name,
                                workoutId.Value!,
                                we.Notes);

                    plan.AddExerciseToDay(request.DayOfWeek, exercise.Value!);

                    var exerciseSets = we.ExerciseSetsDto.Select(ws => {
                        var set = WorkoutPlan.CreateWorkoutExerciseSet(
                            exercise.Value!.Id,
                            ws.SetNumber,
                            ws.Reps,
                            ws.Weight,
                            ws.Unit);

                        return set.Value!;
                    });

                    plan.AddSetToExercises(we.DayOfWeek, exercise.Value!.Id, exerciseSets);

                });

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return ResultT<WorkoutId>.Success(workout.Value!.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return ResultT<WorkoutId>.Failure(ex.Message);
            }
        }
    }
}
