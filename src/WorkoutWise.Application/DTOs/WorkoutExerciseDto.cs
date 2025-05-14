namespace WorkoutWise.Application.DTOs
{
    public record WorkoutExerciseDto {
        public string Name { get; init; }
        public string Notes { get; init; }
        public List<ExerciseSetDto> ExerciseSetsDto { get; init; } = [];
    };

}
