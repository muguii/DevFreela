using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.AuthServices;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var passwordHash = new Guid().ToString();

            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand()
            {
                FullName = "Nome",
                BirthDate = DateTime.Now,
                Email = "email",
                Password = "password",
                Role = "client"
            };

            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == createUserCommand.Password))).Returns(passwordHash);

            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            // Act
            int userId = await createUserCommandHandler.Handle(createUserCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(userId >= 0);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == createUserCommand.Password)), Times.Once);
            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw != createUserCommand.Password)), Times.Never);

            userRepositoryMock.Verify(us => us.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
