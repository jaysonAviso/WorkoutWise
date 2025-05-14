using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Aggregates.ValueObjects
{
    public record WorkoutExerciseId(Guid Value) : SecuredId<WorkoutExerciseId>(Value);
}
