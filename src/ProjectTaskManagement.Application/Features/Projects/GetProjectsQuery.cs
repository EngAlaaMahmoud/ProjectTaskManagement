using MediatR;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Application.Features.Projects
{
    public sealed record GetProjectsQuery(Guid UserId) : IRequest<IEnumerable<ProjectDto>>;

    public sealed class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.GetByUserIdAsync(request.UserId);
            return projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                UserId = project.UserId
            });
        }
    }
}
