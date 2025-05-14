namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record ExerciseSetId(Guid Value) : SecuredId<ExerciseSetId>(Value);