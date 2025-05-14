using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;
using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities
{
    public sealed class WorkoutPlan : Entity<WorkoutPlanId>, IAggregateRoot
    {
        public string Name { get; private set; }
        public UserId UserId { get; private set; }
        public int RestDaysPerWeek { get; private set; }

        private readonly List<Workout> _workouts = [];
        public IReadOnlyCollection<Workout> Workouts => _workouts;

        private WorkoutPlan() { }

        public static ResultT<WorkoutPlan> Create(UserId userId, string name, int restDayPerWeek)
        {
            if (string.IsNullOrWhiteSpace(name))
                return WorkoutPlanErrors.NameIsRequired;

            return ResultT<WorkoutPlan>.Success(new WorkoutPlan { Name = name, UserId = userId, RestDaysPerWeek = restDayPerWeek });
        }

        public static ResultT<Workout> CreateWorkout(string name, WorkoutPlanId workoutPlanId, DayOfWeek dayOfWeek) => Workout.Create(name, workoutPlanId, dayOfWeek);

        public static ResultT<WorkoutExercise> CreateWorkoutExercise(string name, WorkoutId workoutId, string notes) => WorkoutExercise.Create(name, workoutId, notes);

        public static ResultT<ExerciseSet> CreateWorkoutExerciseSet(WorkoutExerciseId workoutExerciseId, int setNumber, int reps, double weight, WeightUnit unit) => ExerciseSet.Create(workoutExerciseId, setNumber, reps, weight, unit);

        public ResultT<WorkoutId> AddWorkout(Workout workout)
        {
            if (_workouts.Any(w => w.DayOfWeek == workout.DayOfWeek))
                return WorkoutErrors.WorkoutDayAlreadyAssigned;

            _workouts.Add(workout);
            return ResultT<WorkoutId>.Success(workout.Id);
        }

        public Result AddExerciseToDay(DayOfWeek dayOfWeek, WorkoutExercise workoutExercise)
        {
            var workout = _workouts.FirstOrDefault(w => w.DayOfWeek == dayOfWeek);

            if (workout is null)
                return WorkoutErrors.NotFound;

            workout.AddExercise(workoutExercise);

            return Result.Success();
        }

        public Result AddSetToExercises(DayOfWeek dayOfWeek, WorkoutExerciseId workoutExerciseId, IEnumerable<ExerciseSet> workoutSets)
        {
            var workout = _workouts.FirstOrDefault(w => w.DayOfWeek == dayOfWeek);
            if (workout is null)
                return WorkoutErrors.NotFound;

            var exercise = workout.Excercises.FirstOrDefault(x => x.Id == workoutExerciseId);
            if (exercise is null)
                return WorkoutExerciseErrors.NotFound;

            exercise.AddSets(workoutSets);

            return Result.Success();
        }
    }
}
