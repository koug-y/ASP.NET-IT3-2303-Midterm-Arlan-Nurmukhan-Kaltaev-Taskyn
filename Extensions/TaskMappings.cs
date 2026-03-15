using TaskTrackerApi.Contracts.Responses;
using TaskTrackerApi.Domain.Entities;

namespace TaskTrackerApi.Extensions;

public static class TaskMappings
{
    public static TaskDto ToDto(this BaseTask task) => task switch
    {
        BugReportTask bugReportTask => new TaskDto(
            bugReportTask.Id,
            nameof(BugReportTask),
            bugReportTask.Title,
            bugReportTask.CreatedAt,
            bugReportTask.IsCompleted,
            bugReportTask.SeverityLevel,
            null),
        FeatureRequestTask featureRequestTask => new TaskDto(
            featureRequestTask.Id,
            nameof(FeatureRequestTask),
            featureRequestTask.Title,
            featureRequestTask.CreatedAt,
            featureRequestTask.IsCompleted,
            null,
            featureRequestTask.EstimatedHours),
        _ => throw new NotSupportedException($"Unsupported task type: {task.GetType().Name}")
    };

    public static IReadOnlyCollection<TaskDto> ToDtoCollection(this IEnumerable<BaseTask> tasks) =>
        tasks.Select(ToDto).ToArray();
}
