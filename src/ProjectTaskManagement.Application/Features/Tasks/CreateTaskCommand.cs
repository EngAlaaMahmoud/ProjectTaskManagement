using MediatR;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Features.Tasks
{
    public sealed record CreateTaskCommand(string Title, string? Description, ProjectTaskManagement.Domain.Enums.TaskStatus Status, DateTime? DueDate, ProjectTaskManagement.Domain.Enums.TaskPriority Priority, Guid ProjectId, Guid UserId) : IRequest<TaskDto>;

    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                Status = request.Status,
                DueDate = request.DueDate,
                Priority = request.Priority,
                ProjectId = request.ProjectId
            };

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                Priority = task.Priority.ToString(),
                ProjectId = task.ProjectId
            };
        }
    }
}
