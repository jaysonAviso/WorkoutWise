using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities
{
    public sealed class WorkoutExercise : Entity<WorkoutExerciseId>
    {
        public string Name { get; private set; }
        public WorkoutId WorkoutId { get; private set; }
        public string? Notes { get; private set; }

        private readonly List<ExerciseSet> _sets;
        public IReadOnlyCollection<ExerciseSet> Sets => _sets;

        private WorkoutExercise() { }

        internal static ResultT<WorkoutExercise> Create(string name, WorkoutId workoutId, string notes)
        {
            return ResultT<WorkoutExercise>.Success(new WorkoutExercise { Name = name, WorkoutId = workoutId, Notes = notes });
        }

        internal Result AddSet(ExerciseSet workoutSet)
        {
            if (workoutSet.SetNumber <= 0)
                return WorkoutSetErrors.RepsMustGreaterthanZero;

            if (workoutSet.Weight < 0)
                return WorkoutSetErrors.WeightConnotBeNegative;

            if (_sets.Any(x => x.SetNumber == workoutSet.SetNumber))
                return WorkoutSetErrors.DuplicateSet;

            _sets.Add(workoutSet);
            return Result.Success();
        }

        internal Result AddSets(IEnumerable<ExerciseSet> workoutSet)
        {
            _sets.AddRange(workoutSet);

            return Result.Success();
        }
    }
}