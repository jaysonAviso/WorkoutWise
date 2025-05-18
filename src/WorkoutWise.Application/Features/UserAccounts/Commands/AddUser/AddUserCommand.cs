using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Commands.AddUser;

public sealed record AddUserCommand(
    string Username, 
    string Password, 
    string? ProfileImageUrl, 
    string FirstName,
    string MiddleName,
    string LastName
) : IRequest<ResultT<UserId>>;
