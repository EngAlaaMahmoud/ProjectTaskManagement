using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Features.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/projects/{projectId:guid}/tasks")]
    public sealed class TasksController : ControllerBase
    {
        private readonly ISender _sender;

        public TasksController(ISender sender)
        {
            _sender = sender;
        }

        private Guid CurrentUserId => Guid.Parse(User.FindFirst("userId")?.Value ?? throw new UnauthorizedAccessException());

        [HttpPost]
        public async Task<IActionResult> Create(Guid projectId, [FromBody] CreateTaskCommand command)
        {
            var request = command with { ProjectId = projectId, UserId = CurrentUserId };
            var task = await _sender.Send(request);
            return Ok(ApiResponse<TaskDto>.SuccessResponse(task, "Task created."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid projectId)
        {
            var tasks = await _sender.Send(new GetTasksByProjectQuery(projectId, CurrentUserId));
            return Ok(ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(tasks));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid projectId, Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (projectId == Guid.Empty)
            {
                throw new InvalidOperationException("Project id is required.");
            }

            var request = command with { Id = id, ProjectId = projectId, UserId = CurrentUserId };
            var task = await _sender.Send(request);
            return Ok(ApiResponse<TaskDto>.SuccessResponse(task, "Task updated."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid projectId, Guid id)
        {
            await _sender.Send(new DeleteTaskCommand(id, projectId, CurrentUserId));
            return Ok(ApiResponse<string>.SuccessResponse("Task deleted."));
        }
    }
}
