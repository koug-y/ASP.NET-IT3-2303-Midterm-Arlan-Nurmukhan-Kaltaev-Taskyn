using TaskTrackerApi.Domain.Entities;
using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Repositories;

public interface ITaskRepository
{
    Task<IReadOnlyCollection<BaseTask>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BaseTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BugReportTask> AddBugReportAsync(string title, SeverityLevel severityLevel, CancellationToken cancellationToken = default);

    Task<FeatureRequestTask> AddFeatureRequestAsync(string title, decimal estimatedHours, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(BaseTask task, CancellationToken cancellationToken = default);
}
