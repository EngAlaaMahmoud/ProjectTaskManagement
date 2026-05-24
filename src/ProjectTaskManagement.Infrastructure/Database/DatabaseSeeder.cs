using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Data;
using BCrypt.Net;

namespace ProjectTaskManagement.Infrastructure.Database
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ProjectTaskManagementDbContext dbContext)
        {
            if (await dbContext.Users.AnyAsync())
            {
                return;
            }

            var demoUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "demo.user",
                Email = "demo@local.test",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd!"),
                CreatedAt = DateTime.UtcNow
            };

            var demoProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Demo Project",
                Description = "This project is seeded for local development.",
                CreatedAt = DateTime.UtcNow,
                UserId = demoUser.Id,
                User = demoUser
            };

            var demoTask = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Title = "Seeded demo task",
                Description = "This task belongs to the seeded demo project.",
                Status = Domain.Enums.TaskStatus.Todo,
                Priority = Domain.Enums.TaskPriority.Medium,
                ProjectId = demoProject.Id,
                Project = demoProject
            };

            await dbContext.Users.AddAsync(demoUser);
            await dbContext.Projects.AddAsync(demoProject);
            await dbContext.Tasks.AddAsync(demoTask);
            await dbContext.SaveChangesAsync();
        }
    }
}
