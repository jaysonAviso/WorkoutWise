using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.Application.DTOs
{
    public record WorkoutPlanDto {
        public string Name { get; init; }
        public UserId userId { get; init; }
        public int RestDaysPerWeek { get; init; }
        public List<WorkoutDto> WorkoutDto { get; init; } = [];
    };

}
