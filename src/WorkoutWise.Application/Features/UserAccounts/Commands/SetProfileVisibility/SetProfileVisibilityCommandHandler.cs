using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Commands.SetProfileVisibility
{
    internal sealed class SetProfileVisibilityCommandHandler : IRequestHandler<SetProfileVisibilityCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public SetProfileVisibilityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(SetProfileVisibilityCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.userId, cancellationToken);

            if (user is null)
                return Result.Failure("User not found.");

            user.SetProfileVisibility(request.IsPublic);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
