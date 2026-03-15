using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Contracts.Responses;

public sealed record TaskDto(
    Guid Id,
    string Type,
    string Title,
    DateTime CreatedAt,
    bool IsCompleted,
    SeverityLevel? SeverityLevel,
    decimal? EstimatedHours);
