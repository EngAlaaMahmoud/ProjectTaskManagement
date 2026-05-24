using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectTaskManagementDbContext _dbContext;

        public UnitOfWork(ProjectTaskManagementDbContext dbContext, IUserRepository users, IProjectRepository projects, ITaskRepository tasks)
        {
            _dbContext = dbContext;
            Users = users;
            Projects = projects;
            Tasks = tasks;
        }

        public IUserRepository Users { get; }
        public IProjectRepository Projects { get; }
        public ITaskRepository Tasks { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
