using DevFreela.Application.Queries.GetUserById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async Task UserWithIdTwoExists_Executed_ReturnUserViewModelOfUserTwo() // GIVEN_WHEN_THEN 
        {
            // Arrange
            int mockId = 2;
            var user = new User("Nome", "email", DateTime.Now, "password", "role");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(user);

            var getUserByIdQuery = new GetUserByIdQuery(mockId);
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            // Act
            var userViewModel = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(userViewModel);
            Assert.Equal(user.Email, userViewModel.Email);
            Assert.Equal(user.FullName, userViewModel.FullName);

            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);
        }

        [Fact]
        public async Task UserWithIdFourNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN 
        {
            // Arrange
            int mockId = 4;
            User user = null;

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.GetByIdAsync(It.Is<int>(id => id == mockId))).ReturnsAsync(user);

            var getUserByIdQuery = new GetUserByIdQuery(mockId);
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            // Act
            var userViewModel = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.Null(userViewModel);

            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);
        }
    }
}
