namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public record WorkoutLogId(Guid Value) : SecuredId<WorkoutLogId>(Value);
