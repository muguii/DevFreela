using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email inválido!");
            RuleFor(user => user.Password).Must(ValidPassword).WithMessage("Senha deve conter pelo menos 8 caracteres, 1 número, 1 letra maiúscula e minúscula e um caractere especial.");
            RuleFor(user => user.FullName).NotEmpty().NotNull().WithMessage("Nome é obrigatório!");
        }

        public bool ValidPassword(string password)
        {
            Regex regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}
