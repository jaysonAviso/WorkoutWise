using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WorkoutWise.Application.Features.UserAccounts.Commands.AddUser;
using WorkoutWise.Application.Features.UserAccounts.Commands.DeactivateUser;
using WorkoutWise.Application.Features.UserAccounts.Commands.SetProfileVisibility;
using WorkoutWise.Application.Features.UserAccounts.Queries;
using WorkoutWise.Application.Features.UserAccounts.Queries.GetUserById;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.API.Controllers.UserAccounts;

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

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.ErrorMesage!);

        return CreatedAtRoute(
            routeName: "GetUserById",
            routeValues: new { id = result.Value! },
            value: result.Value!
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

    [HttpPost("SetPublicProfile")]
    public async Task<IActionResult> SetPublicProfileAsync([FromBody] SetProfileVisibilityCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.ToString()) : BadRequest(result.ToString());
    }

    [HttpPost("DeactivateAccount")]
    public async Task<IActionResult> DeactivateAccountAsync([FromBody] DeactivateUserCommand command)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.ToString()) : BadRequest(result.ToString());
    }
}