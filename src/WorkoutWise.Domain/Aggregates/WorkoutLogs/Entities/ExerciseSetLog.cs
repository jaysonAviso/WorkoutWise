using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Aggregates.Workouts.Entities;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;
using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Domain.Aggregates.WorkoutLogs.Entities;

public sealed class ExerciseSetLog : Entity<ExerciseSetLogId>
{
    public int SetNumber { get; private set; }
    public int Reps { get; private set; }
    public double Weight { get; private set; }
    public WeightUnit Unit { get; private set; }

    public WorkoutExerciseLogId WorkoutExerciseLogId { get; private set; }

    public ExerciseSetLog() { }

    internal static ResultT<ExerciseSetLog> Create(WorkoutExerciseLogId workoutExerciseLogId, int setNumber, int reps, double weight, WeightUnit unit)
    {
        if (setNumber <= 0)
            return WorkoutSetErrors.RepsMustGreaterthanZero;

        if (weight < 0)
            return WorkoutSetErrors.WeightConnotBeNegative;

        return ResultT<ExerciseSetLog>.Success(new ExerciseSetLog() { WorkoutExerciseLogId = workoutExerciseLogId, SetNumber = setNumber, Reps = reps, Weight = weight, Unit = unit });
    }
}
