using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsInProgress_Executed_ChangeStatusToCancelled() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var deleteProjectCommand = new DeleteProjectCommand(mockId);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            project.Start();
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.Cancelled);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsCreated_Executed_DontChangeStatusToCancelled() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var deleteProjectCommand = new DeleteProjectCommand(mockId);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.Created);
            Assert.True(project.Status != ProjectStatusEnum.Cancelled);
            Assert.Null(project.StartedAt);
            Assert.Null(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }
    }
}
