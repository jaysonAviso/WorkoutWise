using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;

public static class WorkoutPlanErrors
{
    public static readonly Error NameIsRequired = new("Workout plan name is required.");
    public static readonly Error NotFound = new("Workout Plan not found.");
}
