using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Features.Projects;

namespace ProjectTaskManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/projects")]
    public sealed class ProjectsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectsController(ISender sender)
        {
            _sender = sender;
        }

        private Guid CurrentUserId => Guid.Parse(User.FindFirst("userId")?.Value ?? throw new UnauthorizedAccessException());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
        {
            var request = command with { UserId = CurrentUserId };
            var project = await _sender.Send(request);
            return Ok(ApiResponse<ProjectDto>.SuccessResponse(project, "Project created."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _sender.Send(new GetProjectsQuery(CurrentUserId));
            return Ok(ApiResponse<IEnumerable<ProjectDto>>.SuccessResponse(projects));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _sender.Send(new GetProjectByIdQuery(id, CurrentUserId));
            return Ok(ApiResponse<ProjectDto>.SuccessResponse(project));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectCommand command)
        {
            var request = command with { Id = id, UserId = CurrentUserId };
            var project = await _sender.Send(request);
            return Ok(ApiResponse<ProjectDto>.SuccessResponse(project, "Project updated."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeleteProjectCommand(id, CurrentUserId));
            return Ok(ApiResponse<string>.SuccessResponse("Project deleted."));
        }
    }
}
