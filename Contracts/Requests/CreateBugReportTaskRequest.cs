using System.ComponentModel.DataAnnotations;
using TaskTrackerApi.Domain.Enums;

namespace TaskTrackerApi.Contracts.Requests;

public sealed record CreateBugReportTaskRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    [EnumDataType(typeof(SeverityLevel))]
    public SeverityLevel SeverityLevel { get; init; }
}
