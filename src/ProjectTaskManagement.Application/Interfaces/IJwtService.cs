using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
