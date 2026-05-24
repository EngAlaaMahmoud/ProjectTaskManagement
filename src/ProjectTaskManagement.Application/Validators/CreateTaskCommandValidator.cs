using FluentValidation;
using ProjectTaskManagement.Application.Features.Tasks;

namespace ProjectTaskManagement.Application.Validators
{
    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("Project id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(150).WithMessage("Task title must be at most 150 characters.");
        }
    }
}
