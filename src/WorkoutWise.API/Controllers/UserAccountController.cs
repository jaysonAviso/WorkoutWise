using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WorkoutWise.Application.Features.UserAccounts.Commands.AddUser;
using WorkoutWise.Application.Features.UserAccounts.Queries;
using WorkoutWise.Application.Features.UserAccounts.Queries.GetUserById;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.API.Controllers
{
    [Controller]
    [Route("UserAccount")]
    public class UserAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UserId = await _mediator.Send(command);

            return CreatedAtRoute(
                routeName: "GetUserById",
                routeValues: new { id = UserId },
                value: UserId
                );
        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserByIdAsync(UserId id)
        {
            var User = await _mediator.Send(new GetUserByIdQuery(id));

            if (User is null)
                return NotFound();

            return Ok(User);
        }
    }
}
