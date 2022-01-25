using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(project => project.Description).MaximumLength(255).WithMessage("Tamanho máximo da descrição é de 255 caracteres.");
            RuleFor(project => project.Title).MaximumLength(30).WithMessage("Tamanho máximo do titulo é de 30 caracteres.");
        }
    }
}
