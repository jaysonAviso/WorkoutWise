using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.Application.Features.WorkoutPlans.Queries;

public record GetWorkoutPlanQuery(WorkoutPlanId WorkoutPlanId) : IRequest<WorkoutPlanDto>
{

}
