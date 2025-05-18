using MediatR;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Queries.GetUserById;

public sealed record GetUserByIdQuery(UserId UserId) : IRequest<UserDto>;
