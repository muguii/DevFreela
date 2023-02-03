using DevFreela.Core.Entities;
using DevFreela.Infrastructure.AuthServices;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            this._unitOfWork = unitOfWork;
            this._authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            
            var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return user.Id;
        }
    }
}
