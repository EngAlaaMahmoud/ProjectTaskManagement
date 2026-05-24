using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Dtos
{
    public sealed class TaskDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime? DueDate { get; init; }
        public string Priority { get; init; } = string.Empty;
        public Guid ProjectId { get; init; }
    }
}
