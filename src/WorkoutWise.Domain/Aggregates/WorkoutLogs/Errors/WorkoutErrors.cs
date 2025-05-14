using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.Workouts.Errors
{
    public static class WorkoutErrors
    {
        public static readonly Error NameIsRequired = new("Workout name is required.");
        public static readonly Error NewNameNotEmpty = new("New name cannot be empty.");        
        public static readonly Error ExcerciseNameIsRequired = new("Exercise name is required.");
        public static readonly Error workoutDateIsRequired = new("Exercise name is required.");
        public static readonly Error NotFound = new("Exercise not found.");
    }
}
