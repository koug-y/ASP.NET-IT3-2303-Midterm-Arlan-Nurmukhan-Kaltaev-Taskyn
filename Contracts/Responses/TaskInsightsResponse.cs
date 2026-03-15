namespace TaskTrackerApi.Contracts.Responses;

public sealed record TaskInsightsResponse(
    IReadOnlyCollection<TaskDto> HighSeverityOpenBugReports,
    decimal TotalEstimatedHoursForOpenFeatureRequests);
