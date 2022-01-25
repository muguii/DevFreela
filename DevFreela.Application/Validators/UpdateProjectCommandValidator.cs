using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(project => project.Description).MaximumLength(255).WithMessage("Tamanho máximo da descrição é de 255 caracteres.");
            RuleFor(project => project.Title).MaximumLength(30).WithMessage("Tamanho máximo do titulo é de 30 caracteres.");
        }
    }
}
