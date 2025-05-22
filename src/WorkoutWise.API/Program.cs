using Microsoft.EntityFrameworkCore;
using WorkoutWise.API.Middlewares;
using WorkoutWise.Application;
using WorkoutWise.Application.Features.UserAccounts.Commands.AddUser;
using WorkoutWise.Infrastructure;
using WorkoutWise.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });

#if DEBUG
    opt.EnableSensitiveDataLogging();
    opt.LogTo(Console.WriteLine, LogLevel.Information);
#endif
});

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddSingleton<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

await app.RunAsync();
