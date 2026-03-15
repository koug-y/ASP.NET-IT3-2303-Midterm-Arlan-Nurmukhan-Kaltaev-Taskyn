using System.Collections.Concurrent;
using TaskTrackerApi.Domain.Entities;
using TaskTrackerApi.Domain.Enums;
using TaskTrackerApi.Domain.Events;

namespace TaskTrackerApi.Repositories;

public sealed class InMemoryTaskRepository : ITaskRepository
{
    private readonly ConcurrentDictionary<Guid, BaseTask> _tasks = new();
    private readonly ILogger<InMemoryTaskRepository> _logger;

    public InMemoryTaskRepository(IEnumerable<BaseTask> seedData, ILogger<InMemoryTaskRepository> logger)
    {
        _logger = logger;

        foreach (var task in seedData)
        {
            AttachCompletionHandler(task);
            _tasks[task.Id] = task;
        }
    }

    public Task<IReadOnlyCollection<BaseTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyCollection<BaseTask> tasks = _tasks.Values
            .OrderByDescending(task => task.CreatedAt)
            .ToArray();

        return Task.FromResult(tasks);
    }

    public Task<BaseTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _tasks.TryGetValue(id, out var task);
        return Task.FromResult(task);
    }

    public Task<BugReportTask> AddBugReportAsync(
        string title,
        SeverityLevel severityLevel,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var task = new BugReportTask(title, severityLevel);
        AttachCompletionHandler(task);
        _tasks[task.Id] = task;

        return Task.FromResult(task);
    }

    public Task<FeatureRequestTask> AddFeatureRequestAsync(
        string title,
        decimal estimatedHours,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var task = new FeatureRequestTask(title, estimatedHours);
        AttachCompletionHandler(task);
        _tasks[task.Id] = task;

        return Task.FromResult(task);
    }

    public Task<bool> UpdateAsync(BaseTask task, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_tasks.ContainsKey(task.Id))
        {
            return Task.FromResult(false);
        }

        _tasks[task.Id] = task;
        return Task.FromResult(true);
    }

    private void AttachCompletionHandler(BaseTask task)
    {
        task.OnTaskCompleted -= HandleTaskCompleted;
        task.OnTaskCompleted += HandleTaskCompleted;
    }

    private void HandleTaskCompleted(object? sender, TaskCompletedEventArgs eventArgs)
    {
        _logger.LogInformation(
            "Task {TaskId} ({TaskTitle}) completed at {CompletedAt:O}.",
            eventArgs.TaskId,
            eventArgs.Title,
            eventArgs.CompletedAt);
    }
}
