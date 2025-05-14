using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutWise.Application.Features.WorkoutPlans.Commands.CreateWorkoutPlan;
using WorkoutWise.Application.Features.WorkoutPlans.Queries;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.API.Controllers
{
    public class WorkoutPlanController : Controller
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

            var workoutPlanId = await _mediator.Send(command);

            return CreatedAtRoute(
                routeName: "GetWorkoutPlanById",
                routeValues: new { workoutPlanId },
                value: workoutPlanId
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
