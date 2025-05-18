using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Application.DTOs
{
    public record UserDto
    {
        public string Username { get; init; }
        public string? ProfileImageUrl { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public bool HasPublicProfile { get; init; }
    }
}
