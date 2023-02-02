using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsCreated_Executed_ChangeStatusToInProgressAndGivenStartDate() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var startProjectCommand = new StartProjectCommand(mockId);
            var startProjectCommandHandler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await startProjectCommandHandler.Handle(startProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.StartAsync(It.Is<Project>(p => p == project)), Times.Once);
        }

        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsCancelled_Executed_DontChangeStatusToInProgress() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var startProjectCommand = new StartProjectCommand(mockId);
            var startProjectCommandHandler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            project.Start();
            project.Cancel();
            await startProjectCommandHandler.Handle(startProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.Cancelled);
            Assert.True(project.Status != ProjectStatusEnum.InProgress);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.StartAsync(It.Is<Project>(p => p == project)), Times.Once);
        }
    }
}
