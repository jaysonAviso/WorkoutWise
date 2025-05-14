using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Application.Features.WorkoutPlans.Commands.AddWorkout;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.Workouts.Commands;

public record CreateWorkoutCommand(
    string Name,
    UserId UserId,
    DateOnly WorkoutDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
    List<WorkoutExerciseLogDto> WorkoutExerciseLogDto
) : IRequest<ResultT<WorkoutLogId>>;
