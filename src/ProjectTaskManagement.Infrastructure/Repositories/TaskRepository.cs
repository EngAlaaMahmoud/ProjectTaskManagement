using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Repositories
{
    public sealed class TaskRepository : ITaskRepository
    {
        private readonly ProjectTaskManagementDbContext _dbContext;

        public TaskRepository(ProjectTaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ProjectTask task)
        {
            await _dbContext.Tasks.AddAsync(task);
        }

        public async Task DeleteAsync(ProjectTask task)
        {
            _dbContext.Tasks.Remove(task);
            await Task.CompletedTask;
        }

        public Task<ProjectTask?> GetByIdAsync(Guid taskId, Guid projectId, Guid userId)
        {
            return _dbContext.Tasks
                .AsNoTracking()
                .Include(x => x.Project)
                .Where(x => x.Id == taskId && x.ProjectId == projectId && x.Project!.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, Guid userId)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .Include(x => x.Project)
                .Where(x => x.ProjectId == projectId && x.Project!.UserId == userId)
                .OrderByDescending(x => x.DueDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(ProjectTask task)
        {
            _dbContext.Tasks.Update(task);
            await Task.CompletedTask;
        }
    }
}
