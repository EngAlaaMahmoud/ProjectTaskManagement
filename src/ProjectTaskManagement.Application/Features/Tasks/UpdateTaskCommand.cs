using MediatR;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Application.Features.Tasks
{
    public sealed record UpdateTaskCommand(Guid Id, Guid ProjectId, string Title, string? Description, ProjectTaskManagement.Domain.Enums.TaskStatus Status, DateTime? DueDate, ProjectTaskManagement.Domain.Enums.TaskPriority Priority, Guid UserId) : IRequest<TaskDto>;

    public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, request.ProjectId, request.UserId);
            if (task is null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            task.Title = request.Title.Trim();
            task.Description = request.Description?.Trim();
            task.Status = request.Status;
            task.DueDate = request.DueDate;
            task.Priority = request.Priority;

            await _unitOfWork.Tasks.UpdateAsync(task);
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
