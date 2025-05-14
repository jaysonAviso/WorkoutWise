using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors
{
    public static class WorkoutSetErrors
    {
        public static readonly Error NotFound = new("Workout set not found.");
        public static readonly Error DuplicateSet = new("Duplicate exercise set.");
        public static readonly Error RepsMustGreaterthanZero = new("Reps must be greater than zero.");
        public static readonly Error WeightConnotBeNegative = new("Weight cannot be negative.");
    }
}
