using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.DTOs;
using WorkoutWise.Application.Interfaces;
using WorkoutWise.Domain.Aggregates.UserAcount.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Application.Features.UserAccounts.Queries.GetUserById
{
    internal sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetUserByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == request.UserId)
                .Select(u => new UserDto 
                {
                    Username = u.Username,
                    ProfileImageUrl = u.ProfileImageUrl,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    HasPublicProfile = u.HasPublicProfile
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
