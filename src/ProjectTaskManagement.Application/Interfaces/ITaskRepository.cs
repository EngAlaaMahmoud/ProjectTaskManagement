using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(ProjectTask task);
        Task<ProjectTask?> GetByIdAsync(Guid taskId, Guid projectId, Guid userId);
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, Guid userId);
        Task UpdateAsync(ProjectTask task);
        Task DeleteAsync(ProjectTask task);
    }
}
