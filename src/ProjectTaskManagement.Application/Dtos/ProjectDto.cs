using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Dtos
{
    public sealed class ProjectDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid UserId { get; init; }
    }
}
