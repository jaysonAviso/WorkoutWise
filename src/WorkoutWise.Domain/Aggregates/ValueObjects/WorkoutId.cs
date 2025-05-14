using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record WorkoutId(Guid Value) : SecuredId<WorkoutId>(Value);
