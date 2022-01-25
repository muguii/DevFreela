using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(project => project.Content).MaximumLength(255).WithMessage("Tamanho máximo do conteúdo é de 255 caracteres.");
            RuleFor(project => project.IdProject).NotEmpty().NotNull().WithMessage("Projeto é obrigatório!");
            RuleFor(project => project.IdUser).NotEmpty().NotNull().WithMessage("Usuário obrigatório!");
        }
    }
}
