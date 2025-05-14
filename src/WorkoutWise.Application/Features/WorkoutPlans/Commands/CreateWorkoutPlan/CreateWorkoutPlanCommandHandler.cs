using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.WorkoutPlans.Commands.CreateWorkoutPlan
{
    internal sealed class CreateWorkoutPlanCommandHandler : IRequestHandler<CreateWorkoutPlanCommand, ResultT<WorkoutPlanId>>
    {
        private readonly IApplicationDbContext _context;

        public CreateWorkoutPlanCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultT<WorkoutPlanId>> Handle(CreateWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            var workoutPlan = WorkoutPlan.Create(
                    request.UserId,
                    request.Name,
                    request.RestDaysPerWeek
                );

            if (workoutPlan.IsFailure)
                return workoutPlan.ErrorMesage!;

            await _context.WorkoutPlans.AddAsync(workoutPlan.Value!, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ResultT<WorkoutPlanId>.Success(workoutPlan.Value!.Id);
        }
    }
}
