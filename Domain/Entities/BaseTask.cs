using TaskTrackerApi.Domain.Events;

namespace TaskTrackerApi.Domain.Entities;

public abstract class BaseTask
{
    protected BaseTask(string title, Guid? id = null, DateTime? createdAt = null, bool isCompleted = false)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Id = id ?? Guid.NewGuid();
        Title = title.Trim();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        IsCompleted = isCompleted;
    }

    public delegate void TaskCompletedHandler(object? sender, TaskCompletedEventArgs eventArgs);

    public event TaskCompletedHandler? OnTaskCompleted;

    public Guid Id { get; }

    public string Title { get; private set; }

    public DateTime CreatedAt { get; }

    public bool IsCompleted { get; private set; }

    public void CompleteTask()
    {
        if (IsCompleted)
        {
            return;
        }

        IsCompleted = true;
        OnTaskCompleted?.Invoke(this, new TaskCompletedEventArgs(Id, Title, DateTime.UtcNow));
    }
}
