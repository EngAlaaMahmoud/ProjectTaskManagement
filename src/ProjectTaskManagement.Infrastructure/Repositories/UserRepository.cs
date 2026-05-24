using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ProjectTaskManagementDbContext _dbContext;

        public UserRepository(ProjectTaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _dbContext.Users.FindAsync(id).AsTask();
        }
    }
}
