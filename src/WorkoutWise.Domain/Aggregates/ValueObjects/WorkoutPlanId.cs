using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record WorkoutPlanId(Guid Value) : SecuredId<WorkoutPlanId>(Value);
