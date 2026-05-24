using MediatR;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Application.Features.Tasks
{
    public sealed record GetTasksByProjectQuery(Guid ProjectId, Guid UserId) : IRequest<IEnumerable<TaskDto>>;

    public sealed class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, IEnumerable<TaskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTasksByProjectQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _unitOfWork.Tasks.GetByProjectIdAsync(request.ProjectId, request.UserId);
            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                Priority = task.Priority.ToString(),
                ProjectId = task.ProjectId
            });
        }
    }
}
