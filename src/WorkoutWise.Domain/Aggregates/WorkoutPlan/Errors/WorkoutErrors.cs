using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors
{
    public static class WorkoutErrors
    {
        public static readonly Error RequiredName = new("Workout name is requied.");
        public static readonly Error DuplicateName = new("Duplicate workout name.");
        public static readonly Error WorkoutDayAlreadyAssigned = new("Workout day already assigned for this day of week.");
        public static readonly Error NotFound = new("Workout not found.");
    }
}
