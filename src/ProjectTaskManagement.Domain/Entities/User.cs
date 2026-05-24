using ProjectTaskManagement.Domain.Interfaces;

namespace ProjectTaskManagement.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
