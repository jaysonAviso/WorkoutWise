namespace WorkoutWise.Application.DTOs
{
    public record WorkoutExerciseLogDto 
    {
        public string Name { get; init; }
        public string SetNumber { get; init; }
        public string Notes { get; init; }
        public List<ExerciseSetLogDto> ExerciseSetLogsDto { get; init; } = [];
    };

}
