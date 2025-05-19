using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection.Metadata.Ecma335;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Aggregates.WorkoutPlan.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Commands.AddUser
{
    internal sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, ResultT<UserId>>
    {
        private readonly IApplicationDbContext _context;

        public AddUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultT<UserId>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //return await _context.BeginTransactionAsync(async () =>
            //{
                var user = User.Create(
                    request.Username,
                    request.Password,
                    request.ProfileImageUrl,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName
                );

                if (user.IsFailure)
                    return user.ErrorMesage!;

            await _context.Users.AddAsync(user.Value!, cancellationToken);

            await _context.SaveChangesAsync();

            return ResultT<UserId>.Success(user.Value!.Id);

            //}, cancellationToken);
        }
    }
}
