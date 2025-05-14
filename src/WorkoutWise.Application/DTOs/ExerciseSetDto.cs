using WorkoutWise.Domain.Enums;

namespace WorkoutWise.Application.DTOs
{
    public record ExerciseSetDto 
    {
        public int SetNumber { get; init; }
        public int Reps { get; init; }
        public double Weight { get; init; }
        public WeightUnit Unit { get; init; }

    };

}
