using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Common.Results;

namespace WorkoutWise.Domain.Aggregates.UserAcount.Errors;

public static class UserErrors
{
    public static readonly Error FirstnameIsRequired = new("First name is required.");
    public static readonly Error LastnameIsRequired = new("Last name is required.");
    public static readonly Error UsernameIsRequired = new("Username is required.");
}
