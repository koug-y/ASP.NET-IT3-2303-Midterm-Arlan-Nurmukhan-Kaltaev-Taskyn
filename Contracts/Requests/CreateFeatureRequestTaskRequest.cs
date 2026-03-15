using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Contracts.Requests;

public sealed record CreateFeatureRequestTaskRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.25", "10000")]
    public decimal EstimatedHours { get; init; }
}
