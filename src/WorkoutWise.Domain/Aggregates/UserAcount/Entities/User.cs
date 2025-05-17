using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.UserAcount.Errors;
using WorkoutWise.Domain.Aggregates.ValueObjects;
using WorkoutWise.Domain.Common.Entities;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.UserAcount.Entities
{
    public sealed class User : Entity<UserId> , IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool HasPublicProfile { get; private set; }

        private User() { }

        public static ResultT<User> Create(string firstname, string lastname, string middlename, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                return UserErrors.UsernameIsRequired;
            
            if (string.IsNullOrWhiteSpace(lastname))
                return UserErrors.LastnameIsRequired;
            
            if (string.IsNullOrWhiteSpace(username))
                return UserErrors.UsernameIsRequired;

            return ResultT<User>.Success(new User
            {
                Id = UserId.New(),
                FirstName = firstname,
                LastName = lastname,
                MiddleName = middlename,
                Username = username,
                Password = password,
                HasPublicProfile = true
            });
        }
    }
}
