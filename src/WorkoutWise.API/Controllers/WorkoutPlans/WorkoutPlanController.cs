using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutWise.Application.Features.WorkoutPlans.Commands.CreateWorkoutPlan;
using WorkoutWise.Application.Features.WorkoutPlans.Queries;
using WorkoutWise.Application.Features.WorkoutPlans.Queries.GetWorkoutPlanById;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.API.Controllers.WorkoutPlans
{
    [ApiController]
    [Route("WorkoutPlan")]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkoutPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkoutPlan([FromBody] CreateWorkoutPlanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.ErrorMesage!);

            return CreatedAtRoute(
                routeName: "GetWorkoutPlanById",
                routeValues: new { result.Value },
                value: result.Value!
            );
        }

        [HttpGet("{id:guid}", Name = "GetWorkoutPlanById")]
        public async Task<IActionResult> GetWorkoutPlanById(WorkoutPlanId id)
        {
            var result = await _mediator.Send(new GetWorkoutPlanQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
