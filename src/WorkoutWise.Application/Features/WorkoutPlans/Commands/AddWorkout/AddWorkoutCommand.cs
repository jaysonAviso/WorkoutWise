using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.WorkoutPlans.Commands.AddWorkout;

public record AddWorkoutCommand(
    WorkoutPlanId WorkoutPlanId,
    UserId UserId,
    string Name, 
    DayOfWeek DayOfWeek, 
    List<WorkoutExerciseRequest> WorkoutExercisesDto
) : IRequest<ResultT<WorkoutId>>;

public record WorkoutExerciseRequest(
    DayOfWeek DayOfWeek,
    string Name,
    string Notes,
    List<ExerciseSetDto> ExerciseSetsDto
);


