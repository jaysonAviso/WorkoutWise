using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Application.Features.UserAccounts.Commands.AddUser;
using WorkoutWise.Application.Features.UserAccounts.Queries.GetUserById;
using WorkoutWise.Application.Features.WorkoutPlans.Commands.AddWorkout;
using WorkoutWise.Application.Features.WorkoutPlans.Commands.CreateWorkoutPlan;
using WorkoutWise.Application.Features.WorkoutPlans.Queries.GetWorkoutPlanById;
using WorkoutWise.Application.Features.Workouts.Commands.CreateWorkout;
using WorkoutWise.Application.Features.Workouts.Queries.GetWorkoutLogById;
using WorkoutWise.Application.Interfaces;

namespace WorkoutWise.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.RegisterServicesFromAssembly(typeof(AddUserCommandHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(GetUserByIdHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(AddWorkoutCommandHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(CreateWorkoutPlanCommandHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(GetWorkoutPlanHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(CreateWorkoutCommandHandler).Assembly);
            config.RegisterServicesFromAssembly(typeof(GetWorkoutLogByIdHandler).Assembly);
        });

        return services; 
    }
}
