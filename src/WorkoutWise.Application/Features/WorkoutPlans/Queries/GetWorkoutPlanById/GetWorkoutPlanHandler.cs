using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Application.Interfaces;

namespace WorkoutWise.Application.Features.WorkoutPlans.Queries.GetWorkoutPlanById
{
    internal sealed class GetWorkoutPlanHandler : IRequestHandler<GetWorkoutPlanQuery, WorkoutPlanDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetWorkoutPlanHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkoutPlanDto?> Handle(GetWorkoutPlanQuery request, CancellationToken cancellationToken)
        {
            return await _context.WorkoutPlans
                .Include(wp => wp.Workouts)
                .ThenInclude(w => w.Excercises)
                .Where(x => x.Id == request.WorkoutPlanId)
                .Select(wp => new WorkoutPlanDto
                {
                    Name = wp.Name,
                    userId = wp.UserId,
                    RestDaysPerWeek = wp.RestDaysPerWeek,
                    WorkoutDto = wp.Workouts.Select(w => new WorkoutDto
                    {
                        Name = w.Name,
                        DayOfWeek = w.DayOfWeek,
                        WorkoutExercisesDto = w.Excercises.Select(we => new WorkoutExerciseDto
                        {
                            Name = we.Name,
                            Notes = we.Notes,
                            ExerciseSetsDto = we.Sets.Select(es => new ExerciseSetDto
                            {
                                SetNumber = es.SetNumber,
                                Reps = es.Reps,
                                Weight = es.Weight,
                                Unit = es.Unit
                            }).ToList()
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
