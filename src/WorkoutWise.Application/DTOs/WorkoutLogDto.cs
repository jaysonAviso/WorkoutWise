using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutWise.Domain.Aggregates.ValueObjects;

namespace WorkoutWise.Application.DTOs
{
    public record WorkoutLogDto
    {
        public WorkoutLogId Id { get; init; }
        public Guid UserId { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateOnly WorkoutDate { get; init; }
        public TimeOnly? StartTime { get; init; }
        public TimeOnly? EndTime { get; init; }
        public List<WorkoutExerciseLogDto> WorkoutExerciseDto { get; init; } = [];
    };
}
