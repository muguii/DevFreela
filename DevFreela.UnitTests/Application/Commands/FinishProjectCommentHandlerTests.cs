using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class FinishProjectCommentHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsInProgress_Executed_ChangeStatusToFinishAndGivenFinishDate() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var finishProjectCommand = new FinishProjectCommand(mockId);
            var finishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            project.Start();
            await finishProjectCommandHandler.Handle(finishProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.Finish);
            Assert.NotNull(project.StartedAt);
            Assert.NotNull(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ProjectWithIdNineExistsAndStatusIsCreated_Executed_DontChangeStatusToFinish() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var finishProjectCommand = new FinishProjectCommand(mockId);
            var finishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await finishProjectCommandHandler.Handle(finishProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.True(project.Status == ProjectStatusEnum.Created);
            Assert.True(project.Status != ProjectStatusEnum.Finish);
            Assert.Null(project.StartedAt);
            Assert.Null(project.FinishAt);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }
    }
}
