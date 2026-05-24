using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Repositories
{
    public sealed class ProjectRepository : IProjectRepository
    {
        private readonly ProjectTaskManagementDbContext _dbContext;

        public ProjectRepository(ProjectTaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
        }

        public async Task DeleteAsync(Project project)
        {
            _dbContext.Projects.Remove(project);
            await Task.CompletedTask;
        }

        public Task<Project?> GetByIdAsync(Guid projectId, Guid userId)
        {
            return _dbContext.Projects
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == projectId && x.UserId == userId);
        }

        public async Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _dbContext.Projects.Update(project);
            await Task.CompletedTask;
        }
    }
}
