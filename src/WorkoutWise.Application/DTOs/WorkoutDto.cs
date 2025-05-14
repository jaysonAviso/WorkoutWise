using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.Application.DTOs
{
    public record WorkoutDto
    {
        public WorkoutPlanId WorkoutPlanId { get; init; }
        public UserId UserId { get; init; }
        public string Name { get; init; }
        public DayOfWeek DayOfWeek { get; init; }
        public List<WorkoutExerciseDto> WorkoutExercisesDto { get; init; } = [];
    };

}
