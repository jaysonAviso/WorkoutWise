using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutLogs.Entities;
using WorkoutWise.Domain.Aggregates.Workouts.Errors;
using WorkoutWise.Domain.Common.AppTime;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;
using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Domain.Aggregates.Workouts.Entities;

public sealed class WorkoutLog : Entity<WorkoutLogId>, IAggregateRoot
{
    public UserId UserId { get; private set; }
    public string Name { get; private set; }
    public WorkoutPlanId WorkoutPlanId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public DateOnly WorkoutDate { get; set; }
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }

    private readonly List<WorkoutExerciseLog> _workoutExerciseLogs = [];
    public IReadOnlyCollection<WorkoutExerciseLog> WorkoutExerciseLogs => [.. _workoutExerciseLogs];

    private WorkoutLog() { }

    public static ResultT<WorkoutLog> Create(UserId userId, string name, DateOnly workoutDate, TimeOnly startTime, TimeOnly endTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            return WorkoutErrors.NameIsRequired;

        if (workoutDate == default)
            return WorkoutErrors.workoutDateIsRequired;

        return ResultT<WorkoutLog>.Success(new WorkoutLog { Id = WorkoutLogId.New(), UserId = userId, Name = name, WorkoutDate = workoutDate, StartTime = startTime, EndTime = endTime });
    }

    public static ResultT<WorkoutExerciseLog> CreateWorkoutExerciseLog(WorkoutLogId workoutId, string exerciseName, string notes) => WorkoutExerciseLog.Create(workoutId, exerciseName, notes);
    
    public static ResultT<ExerciseSetLog> CreateExerciseSetLog(WorkoutExerciseLogId workoutExerciseLogId, int setNumber, int reps, double weight, WeightUnit unit) => ExerciseSetLog.Create(workoutExerciseLogId, setNumber, reps, weight, unit);

    public ResultT<WorkoutLog> Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return WorkoutErrors.NewNameNotEmpty;

        Name = newName;
        UpdateTimestamp();

        return ResultT<WorkoutLog>.Success(this);
    }

    public ResultT<WorkoutExerciseLogId> AddWorkoutExercise(string exerciseName, string notes)
    {
        if (string.IsNullOrWhiteSpace(exerciseName))
            return WorkoutErrors.ExcerciseNameIsRequired;

        var exerciseLog = WorkoutExerciseLog.Create(Id, exerciseName, notes);

        _workoutExerciseLogs.Add(exerciseLog.Value!);

        return ResultT<WorkoutExerciseLogId>.Success(exerciseLog.Value!.Id);
    }
    public Result RemoveWorkoutExerciseLog(WorkoutExerciseLogId workoutExerciseLogId)
    {
        var workoutDetail = _workoutExerciseLogs.FirstOrDefault(d => d.Id == workoutExerciseLogId);

        if (workoutDetail is null) return Result.Failure($"Workout detail with ID {workoutExerciseLogId.Value} was not found.");

        _workoutExerciseLogs.Remove(workoutDetail);

        return Result.Success();
    }

    public Result AddSetToExercisesLog(WorkoutExerciseLogId workoutExerciseLogId, IEnumerable<ExerciseSetLog> workoutSets)
    {
        var exercise = _workoutExerciseLogs.FirstOrDefault(x => x.Id == workoutExerciseLogId);
        if (exercise is null)
            return WorkoutErrors.NotFound;

        exercise.AddSets(workoutSets);

        return Result.Success();
    }

}

