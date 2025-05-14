namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record WorkoutExerciseLogId(Guid Value) : SecuredId<WorkoutExerciseLogId>(Value);
