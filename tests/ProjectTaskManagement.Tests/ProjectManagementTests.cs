using Moq;
using ProjectTaskManagement.Application.Features.Auth;
using ProjectTaskManagement.Application.Features.Projects;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using Xunit;

namespace ProjectTaskManagement.Tests
{
    public class ProjectManagementTests
    {
        [Fact]
        public async Task RegisterCommandHandler_CreatesUserAndReturnsToken()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);
            userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var jwtServiceMock = new Mock<IJwtService>();
            jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("test-token");

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(x => x.Users).Returns(userRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var handler = new RegisterCommandHandler(unitOfWorkMock.Object, jwtServiceMock.Object);
            var response = await handler.Handle(new RegisterCommand("tester", "tester@local.test", "Password1!"), CancellationToken.None);

            Assert.Equal("tester", response.Username);
            Assert.Equal("tester@local.test", response.Email);
            Assert.Equal("test-token", response.Token);
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CreateProjectCommandHandler_CreatesProject()
        {
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Project>())).Returns(Task.CompletedTask);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(x => x.Projects).Returns(projectRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var handler = new CreateProjectCommandHandler(unitOfWorkMock.Object);
            var response = await handler.Handle(new CreateProjectCommand("Test Project", "A sample project", Guid.NewGuid()), CancellationToken.None);

            Assert.Equal("Test Project", response.Name);
            Assert.Equal("A sample project", response.Description);
            projectRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task GetProjectsQueryHandler_ReturnsMappedProjectList()
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Existing Project",
                Description = "Existing description",
                CreatedAt = DateTime.UtcNow,
                UserId = Guid.NewGuid()
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new[] { project });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(x => x.Projects).Returns(projectRepositoryMock.Object);

            var handler = new GetProjectsQueryHandler(unitOfWorkMock.Object);
            var result = await handler.Handle(new GetProjectsQuery(project.UserId), CancellationToken.None);

            Assert.Single(result);
            Assert.Equal(project.Name, result.First().Name);
        }
    }
}
