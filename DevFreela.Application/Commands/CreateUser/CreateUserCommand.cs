using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }

        public CreateUserCommand(string fullName, string email, DateTime birthDate, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Role = role;
        }
    }
}
