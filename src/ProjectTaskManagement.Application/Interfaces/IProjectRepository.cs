using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<Project?> GetByIdAsync(Guid projectId, Guid userId);
        Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
    }
}
