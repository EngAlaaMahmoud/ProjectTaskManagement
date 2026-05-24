using ProjectTaskManagement.Domain.Enums;
using ProjectTaskManagement.Domain.Interfaces;

namespace ProjectTaskManagement.Domain.Entities
{
    public class ProjectTask : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ProjectTaskManagement.Domain.Enums.TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public ProjectTaskManagement.Domain.Enums.TaskPriority Priority { get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
