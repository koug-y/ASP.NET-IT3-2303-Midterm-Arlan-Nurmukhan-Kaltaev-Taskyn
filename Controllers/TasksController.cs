using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Contracts.Requests;
using TaskTrackerApi.Contracts.Responses;
using TaskTrackerApi.Domain.Services;
using TaskTrackerApi.Extensions;
using TaskTrackerApi.Repositories;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TaskDto>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);
        return Ok(tasks.ToDtoCollection());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
        return task is null ? NotFound() : Ok(task.ToDto());
    }

    [HttpGet("insights")]
    [ProducesResponseType(typeof(TaskInsightsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TaskInsightsResponse>> GetInsights(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);
        var snapshot = TaskFilterService.Filter(tasks);

        return Ok(new TaskInsightsResponse(
            snapshot.HighSeverityOpenBugReports.Select(task => task.ToDto()).ToArray(),
            snapshot.TotalEstimatedHoursForOpenFeatureRequests));
    }

    [HttpPost("bug")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskDto>> CreateBug(
        [FromBody] CreateBugReportTaskRequest request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.AddBugReportAsync(
            request.Title,
            request.SeverityLevel,
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.ToDto());
    }

    [HttpPost("feature")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskDto>> CreateFeature(
        [FromBody] CreateFeatureRequestTaskRequest request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.AddFeatureRequestAsync(
            request.Title,
            request.EstimatedHours,
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.ToDto());
    }

    [HttpPut("{id:guid}/complete")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> Complete(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        task.CompleteTask();
        await _taskRepository.UpdateAsync(task, cancellationToken);

        return Ok(task.ToDto());
    }
}
