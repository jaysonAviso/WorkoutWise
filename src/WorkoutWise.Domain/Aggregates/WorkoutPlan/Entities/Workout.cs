using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities
{
    public sealed class Workout : Entity<WorkoutId>
    {
        public string Name { get; private set; }
        public WorkoutPlanId WorkoutPlanId { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }

        private readonly List<WorkoutExercise> _exercises;
            
        public IReadOnlyCollection<WorkoutExercise> Excercises => _exercises;

        private Workout() { }

        internal static ResultT<Workout> Create(string name, WorkoutPlanId workoutPlanId, DayOfWeek dayOfWeek)
        {
            if (string.IsNullOrWhiteSpace(name))
                return WorkoutErrors.RequiredName;

            return ResultT<Workout>.Success(new Workout() { Name = name, WorkoutPlanId = workoutPlanId, DayOfWeek = dayOfWeek });
        }

        internal Result AddExercise(WorkoutExercise workoutExercise)
        {
            if (_exercises.Any(e => e.Name == workoutExercise.Name))
                return WorkoutExerciseErrors.DuplicateName;

            _exercises.Add(workoutExercise);

            return Result.Success();
        }
    }
}