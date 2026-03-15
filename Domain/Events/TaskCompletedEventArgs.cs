namespace TaskTrackerApi.Domain.Events;

public sealed class TaskCompletedEventArgs : EventArgs
{
    public TaskCompletedEventArgs(Guid taskId, string title, DateTime completedAt)
    {
        TaskId = taskId;
        Title = title;
        CompletedAt = completedAt;
    }

    public Guid TaskId { get; }

    public string Title { get; }

    public DateTime CompletedAt { get; }
}
