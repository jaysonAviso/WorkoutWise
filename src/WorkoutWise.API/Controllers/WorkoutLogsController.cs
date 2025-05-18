using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutWise.Application.Features.Workouts.Commands.CreateWorkout;
using WorkoutWise.Application.Features.Workouts.Queries.GetWorkoutLogById;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.API.Controllers;

[ApiController]
[Route("WorkoutLogs")]
public class WorkoutLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkoutLogAsync([FromBody] CreateWorkoutCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var workoutLogId = await _mediator.Send(command);

        return CreatedAtRoute(
            routeName: "GetWorkoutLogById",
            routeValues: new { id = workoutLogId }, 
            value: workoutLogId);
    }

    [HttpGet("{id:guid}", Name = "GetWorkoutLogById")]
    public async Task<IActionResult> GetWorkoutLogByIdAsync(WorkoutLogId id)
    {
        var workoutLog = await _mediator.Send(new GetWorkoutLogByIdQuery(id));

        if (workoutLog is null)
            return NotFound();

        return Ok(workoutLog);
    }
}
