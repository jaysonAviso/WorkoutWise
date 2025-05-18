using System.Collections.Generic;
using System.Runtime.InteropServices;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;
using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities
{
    public sealed class ExerciseSet : Entity<ExerciseSetId>
    {
        public int SetNumber { get; private set; }
        public int Reps { get; private set; }
        public double Weight { get; private set; }
        public WeightUnit Unit { get; private set; }
        public WorkoutExerciseId WorkoutExerciseId { get; private set; }

        private ExerciseSet() { }

        internal static ResultT<ExerciseSet> Create(WorkoutExerciseId workoutExerciseId, int setNumber, int reps, double weight, WeightUnit unit)
        {
            if (setNumber <= 0)
                return WorkoutSetErrors.RepsMustGreaterthanZero;

            if (weight < 0)
                return WorkoutSetErrors.WeightConnotBeNegative;

            return ResultT<ExerciseSet>.Success(new ExerciseSet() { Id = ExerciseSetId.New(), WorkoutExerciseId = workoutExerciseId, SetNumber = setNumber, Reps = reps, Weight = weight, Unit = unit });
        }
    }
}