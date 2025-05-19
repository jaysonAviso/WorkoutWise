using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Commands.SetProfileVisibility;

public sealed record SetProfileVisibilityCommand(UserId userId, bool IsPublic) : IRequest<Result>;
