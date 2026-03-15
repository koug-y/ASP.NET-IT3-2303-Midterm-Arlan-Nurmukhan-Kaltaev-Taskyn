using TaskTrackerApi.Domain.Entities;
using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Domain.Services;

public static class TaskFilterService
{
    public static TaskFilterSnapshot Filter(IEnumerable<BaseTask> tasks)
    {
        var taskList = tasks.ToArray();

        var highSeverityOpenBugReports = taskList
            .OfType<BugReportTask>()
            .Where(task => !task.IsCompleted && task.SeverityLevel == SeverityLevel.High)
            .OrderByDescending(task => task.CreatedAt)
            .ToArray();

        var totalEstimatedHoursForOpenFeatureRequests = taskList
            .OfType<FeatureRequestTask>()
            .Where(task => !task.IsCompleted)
            .Sum(task => task.EstimatedHours);

        return new TaskFilterSnapshot(highSeverityOpenBugReports, totalEstimatedHoursForOpenFeatureRequests);
    }
}
