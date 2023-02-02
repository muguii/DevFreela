using DevFreela.Core.Entities;
using System.Collections.Generic;
using Xunit;
using Moq;
using DevFreela.Core.Repositories;
using DevFreela.Application.Queries.GetAllProjects;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project("Titulo 1", "Descricao 1", 1, 2, 10000M),
                new Project("Titulo 2", "Descricao 2", 1, 2, 30000M),
                new Project("Titulo 3", "Descricao 3", 1, 2, 30000M),
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>()).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery(string.Empty);
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModels = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(projectViewModels);
            Assert.NotEmpty(projectViewModels);
            Assert.Equal(projects.Count, projectViewModels.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>()).Result, Times.Once);
        }
    }
}
