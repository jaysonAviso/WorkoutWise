using System.Collections.Generic;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutLogs.Entities;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.Workouts.Entities;

public sealed class WorkoutExerciseLog : Entity<WorkoutExerciseLogId>
{
    public WorkoutLogId WorkoutId { get; private set; }
    public string ExerciseName { get; private set; }
    public string Notes { get; private set; }

    private readonly List<ExerciseSetLog> _exerciseSetLogs = [];
    public IReadOnlyCollection<ExerciseSetLog> ExerciseSetLogs => [.. _exerciseSetLogs];

    private WorkoutExerciseLog() { }

    public static ResultT<WorkoutExerciseLog> Create(WorkoutLogId workoutId, string exerciseName, string notes)
    {
        return ResultT<WorkoutExerciseLog>.Success(new WorkoutExerciseLog { Id = WorkoutExerciseLogId.New(), WorkoutId = workoutId, ExerciseName = exerciseName , Notes = notes});
    }

    internal Result AddSet(ExerciseSetLog workoutSet)
    {
        if (workoutSet.SetNumber <= 0)
            return WorkoutSetErrors.RepsMustGreaterthanZero;

        if (workoutSet.Weight < 0)
            return WorkoutSetErrors.WeightConnotBeNegative;

        if (_exerciseSetLogs.Any(x => x.SetNumber == workoutSet.SetNumber))
            return WorkoutSetErrors.DuplicateSet;

        _exerciseSetLogs.Add(workoutSet);
        return Result.Success();
    }

    internal Result AddSets(IEnumerable<ExerciseSetLog> workoutSets)
    {
        _exerciseSetLogs.AddRange(workoutSets);

        return Result.Success();
    }
}

