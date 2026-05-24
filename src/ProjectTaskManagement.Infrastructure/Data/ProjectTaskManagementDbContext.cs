using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Infrastructure.Data
{
    public sealed class ProjectTaskManagementDbContext : DbContext
    {
        public ProjectTaskManagementDbContext(DbContextOptions<ProjectTaskManagementDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectTask> Tasks => Set<ProjectTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.Username).IsRequired().HasMaxLength(150);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(255);
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.HasMany(x => x.Projects)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.HasMany(x => x.Tasks)
                    .WithOne(x => x.Project)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.Status).IsRequired();
                entity.Property(x => x.Priority).IsRequired();
            });
        }
    }
}
