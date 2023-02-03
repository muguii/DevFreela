using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.AuthServices;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            this._unitOfWork = unitOfWork;
            this._authService = authService;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = await _unitOfWork.Users.GetByEmailAndPasswordAsync(request.Email, passwordHash);

            if (user == null)
                return null;

            var jwtToken = _authService.GenerateJwtToken(request.Email, user.Role);

            return new LoginUserViewModel(user.Email, jwtToken);
        }
    }
}
