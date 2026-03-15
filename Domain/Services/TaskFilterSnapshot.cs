using TaskTrackerApi.Domain.Entities;

namespace TaskTrackerApi.Domain.Services;

public sealed record TaskFilterSnapshot(
    IReadOnlyCollection<BugReportTask> HighSeverityOpenBugReports,
    decimal TotalEstimatedHoursForOpenFeatureRequests);
