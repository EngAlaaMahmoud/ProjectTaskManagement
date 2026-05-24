using FluentValidation;
using ProjectTaskManagement.Application.Features.Tasks;

namespace ProjectTaskManagement.Application.Validators
{
    public sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Task id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(150).WithMessage("Task title must be at most 150 characters.");
        }
    }
}
