using TaskTrackerApi.Domain.Entities;
using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Infrastructure;

public static class SeedData
{
    public static IReadOnlyCollection<BaseTask> Create()
    {
        var now = DateTime.UtcNow;

        return new BaseTask[]
        {
            new BugReportTask(
                title: "Checkout page throws a null reference exception",
                severityLevel: SeverityLevel.High,
                createdAt: now.AddHours(-1)),
            new FeatureRequestTask(
                title: "Bulk import tasks from CSV",
                estimatedHours: 10m,
                createdAt: now.AddHours(-2)),
            new BugReportTask(
                title: "Dashboard filters reset after refresh",
                severityLevel: SeverityLevel.Medium,
                createdAt: now.AddHours(-5)),
            new BugReportTask(
                title: "Notifications drawer overlaps mobile footer",
                severityLevel: SeverityLevel.High,
                createdAt: now.AddHours(-8)),
            new FeatureRequestTask(
                title: "Kanban swimlane customization",
                estimatedHours: 6.5m,
                createdAt: now.AddHours(-12),
                isCompleted: true)
        };
    }
}
