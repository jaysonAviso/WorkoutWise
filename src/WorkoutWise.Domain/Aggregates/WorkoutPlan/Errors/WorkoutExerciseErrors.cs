using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.WorkoutPlan.Errors;

public static class WorkoutExerciseErrors
{
    public static readonly Error DuplicateName = new("Duplicate exercise name.");
    public static readonly Error NotFound = new("Exercise not found.");
}
