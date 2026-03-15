namespace TaskTrackerApi.Domain.Entities;

public sealed class FeatureRequestTask : BaseTask
{
    public FeatureRequestTask(
        string title,
        decimal estimatedHours,
        Guid? id = null,
        DateTime? createdAt = null,
        bool isCompleted = false)
        : base(title, id, createdAt, isCompleted)
    {
        if (estimatedHours <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(estimatedHours), "Estimated hours must be greater than zero.");
        }

        EstimatedHours = estimatedHours;
    }

    public decimal EstimatedHours { get; }
}
