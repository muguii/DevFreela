using DevFreela.Core.Entities;
using System.Collections.Generic;
using Xunit;
using Moq;
using DevFreela.Core.Repositories;
using DevFreela.Application.Queries.GetAllProjects;
using System.Threading.Tasks;
using DevFreela.Core.Models;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreePaginationProjectViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var paginationProjects = new PaginationResult<Project>()
            {
                Data = new List<Project>
                {
                    new Project("Titulo 1", "Descricao 1", 1, 2, 10000M),
                    new Project("Titulo 2", "Descricao 2", 1, 2, 30000M),
                    new Project("Titulo 3", "Descricao 3", 1, 2, 30000M),
                }
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(paginationProjects);

            var getAllProjectsQuery = new GetAllProjectsQuery();
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var paginationProjectViewModels = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(paginationProjectViewModels);
            Assert.NotEmpty(paginationProjectViewModels.Data);
            Assert.Equal(paginationProjects.Data.Count, paginationProjectViewModels.Data.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}
