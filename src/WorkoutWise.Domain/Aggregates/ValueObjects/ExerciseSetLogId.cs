namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record ExerciseSetLogId(Guid Value) : SecuredId<ExerciseSetLogId>(Value);