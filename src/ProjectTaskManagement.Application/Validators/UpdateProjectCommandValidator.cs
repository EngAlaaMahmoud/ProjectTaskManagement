using FluentValidation;
using ProjectTaskManagement.Application.Features.Projects;

namespace ProjectTaskManagement.Application.Validators
{
    public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Project id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(150).WithMessage("Project name must be at most 150 characters.");
        }
    }
}
