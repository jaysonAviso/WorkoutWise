using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Application.Interfaces;

namespace WorkoutWise.Application.Features.Workouts.Queries.GetWorkoutLogById;

internal sealed class GetWorkoutLogByIdHandler : IRequestHandler<GetWorkoutLogByIdQuery, WorkoutLogDto?>
{
    private readonly IApplicationDbContext _context;

    public GetWorkoutLogByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutLogDto?> Handle(GetWorkoutLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workouts
            .AsNoTracking()
            .Include(w => w.WorkoutExerciseLogs)
            .ThenInclude(we => we.ExerciseSetLogs)
            .Where(x => x.Id == request.Id)
            .Select(w => new WorkoutLogDto
            {
                Id = w.Id,
                Name = w.Name,
                WorkoutDate = w.WorkoutDate,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                WorkoutExerciseDto = w.WorkoutExerciseLogs.Select(we => new WorkoutExerciseLogDto 
                {
                    Name = we.ExerciseName,
                    Notes = we.Notes,
                    ExerciseSetLogsDto = we.ExerciseSetLogs.Select(esl => new ExerciseSetLogDto 
                    {
                        SetNumber = esl.SetNumber,
                        Reps = esl.Reps,
                        Weight = esl.Weight,
                        Unit = esl.Unit
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
