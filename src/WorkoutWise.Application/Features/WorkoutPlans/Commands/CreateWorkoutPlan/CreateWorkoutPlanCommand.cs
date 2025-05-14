using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.WorkoutPlans.Commands.CreateWorkoutPlan;

public record CreateWorkoutPlanCommand(
    string Name, 
    UserId UserId, 
    int RestDaysPerWeek
) : IRequest<ResultT<WorkoutPlanId>>;

