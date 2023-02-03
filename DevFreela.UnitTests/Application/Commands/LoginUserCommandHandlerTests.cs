using DevFreela.Application.Commands.LoginUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.AuthServices;
using DevFreela.Infrastructure.Persistence;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task LoginDataIsOk_Executed_GenerateJwtTokenAndReturnLoginUserViewModel()
        {
            // Arrange
            var passwordMock = "password";
            var passwordHashMock = new Guid().ToString();
            var emailMock = "email@email.com";
            var user = new User("Nome", emailMock, DateTime.Now, passwordHashMock, "client");
            var jwtTokenMock = new Guid().ToString();

            var loginUserCommand = new LoginUserCommand() { Email = emailMock, Password = passwordMock };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);
            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password))).Returns(passwordHashMock);
            authServiceMock.Setup(auth => auth.GenerateJwtToken(It.Is<string>(email => email == emailMock), It.Is<string>(role => role == user.Role))).Returns(jwtTokenMock);

            userRepositoryMock.Setup(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)).Result)
                              .Returns(user);

            var loginUserCommandHandler = new LoginUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var loginUserViewModel = await loginUserCommandHandler.Handle(loginUserCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(jwtTokenMock, loginUserViewModel.Token);
            Assert.Equal(emailMock, loginUserViewModel.Email);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password)), Times.Once);
            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw != loginUserCommand.Password)), Times.Never);
            
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(email => email == emailMock), It.Is<string>(role => role == user.Role)), Times.Once);
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(email => email != emailMock), It.Is<string>(role => role == user.Role)), Times.Never);
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(email => email == emailMock), It.Is<string>(role => role != user.Role)), Times.Never);
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(email => email != emailMock), It.Is<string>(role => role != user.Role)), Times.Never);

            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email != emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email != emailMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
        }

        [Fact]
        public async Task LoginDataIsOkAndUserNotExists_Executed_ReturnNull()
        {
            // Arrange
            var passwordMock = "password";
            var passwordHashMock = new Guid().ToString();
            var emailMock = "email@email.com";
            User user = null;

            var loginUserCommand = new LoginUserCommand() { Email = emailMock, Password = passwordMock };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);
            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password))).Returns(passwordHashMock);

            userRepositoryMock.Setup(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)))
                              .ReturnsAsync(user);

            var loginUserCommandHandler = new LoginUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var loginUserViewModel = await loginUserCommandHandler.Handle(loginUserCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.Null(loginUserViewModel);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password)), Times.Once);
            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw != loginUserCommand.Password)), Times.Never);

            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email != emailMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email == emailMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByEmailAndPasswordAsync(It.Is<string>(email => email != emailMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
        }
    }
}
