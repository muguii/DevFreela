using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdQueryHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdNineExists_Executed_ReturnProjectDetailsViewModelOfProjectNine() // GIVEN_WHEN_THEN
        {
            // Arrange
            var project = new Project("Titulo 1", "Descricao 1", 1, 2, 10000M);
            int projectId = 9;

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == projectId)).Result).Returns(project);

            var getProjectByIdQuery = new GetProjectByIdQuery(projectId);
            var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectDetailsViewModel = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(projectDetailsViewModel);
            Assert.Equal(project.Title, projectDetailsViewModel.Title);
            Assert.Equal(project.Description, projectDetailsViewModel.Description);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == projectId)).Result, Times.Once);
        }

        [Fact]
        public async Task ProjectWithIdFiveNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            int projectId = 5;
            Project project = null;

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == projectId))).ReturnsAsync(project);

            var getProjectByIdQuery = new GetProjectByIdQuery(projectId);
            var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectDetailsViewModel = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new System.Threading.CancellationToken());
             
            // Assert
            Assert.Null(projectDetailsViewModel);
        }
    }
}
