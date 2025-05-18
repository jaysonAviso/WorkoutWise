using MediatR;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.Workouts.Commands;

internal sealed class CreateWorkoutCommandHandler: IRequestHandler<CreateWorkoutCommand, ResultT<WorkoutLogId>>
{
    private readonly IApplicationDbContext _context;
    public CreateWorkoutCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultT<WorkoutLogId>> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        return await _context.ExecuteTransactionAsync(async () => 
        {
            var workout = WorkoutLog.Create(
            request.UserId,
            request.Name,
            request.WorkoutDate,
            request.StartTime,
            request.EndTime
        );

            if (workout.IsFailure)
                return workout.ErrorMesage!;

            var errors = new List<string>();

            request.WorkoutExerciseLogDto
                .ForEach(wel =>
                {
                    var exercise = workout.Value!.AddWorkoutExercise(
                        wel.Name,
                        wel.Notes
                    );

                    if (exercise.IsFailure)
                        errors.Add(exercise.ToString()!);

                    var sets = wel.ExerciseSetLogsDto.Select(esl =>
                    {
                        var set = WorkoutLog.CreateExerciseSetLog(
                                exercise.Value!,
                                esl.SetNumber,
                                esl.Reps,
                                esl.Weight,
                                esl.Unit);

                        return set.Value!;

                    });

                    workout.Value!.AddSetToExercisesLog(exercise.Value!, sets);

                });

            if (errors.Count > 0)
                return ResultT<WorkoutLogId>.Failure(string.Join("; ", errors));

            await _context.Workouts.AddAsync(workout.Value!);

            return ResultT<WorkoutLogId>.Success(workout.Value!.Id);
        }, cancellationToken);
    }
}
