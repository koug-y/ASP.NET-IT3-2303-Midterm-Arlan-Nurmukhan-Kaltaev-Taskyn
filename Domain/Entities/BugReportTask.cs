using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Domain.Entities;

public sealed class BugReportTask : BaseTask
{
    public BugReportTask(
        string title,
        SeverityLevel severityLevel,
        Guid? id = null,
        DateTime? createdAt = null,
        bool isCompleted = false)
        : base(title, id, createdAt, isCompleted)
    {
        SeverityLevel = severityLevel;
    }

    public SeverityLevel SeverityLevel { get; }
}
