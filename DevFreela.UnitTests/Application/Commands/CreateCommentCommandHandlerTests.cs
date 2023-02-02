using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_CreateProjectComment() // GIVEN_WHEN_THEN
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createCommentCommand = new CreateCommentCommand()
            {
                Content = "Conteudo",
                IdProject = 5,
                IdUser = 1
            };

            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock.Object);

            // Act
            await createCommentCommandHandler.Handle(createCommentCommand, new System.Threading.CancellationToken());

            // Assert
            projectRepositoryMock.Verify(pr => pr.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
        }
    }
}
