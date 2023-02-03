using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommentHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdNineExists_Executed_ChangeProjectData() // GIVEN_WHEN_THEN
        {
            // Arrange
            int mockId = 9;
            string mockTitle = "Titulo";
            string mockDescription = "Descricao";
            int mockClientId = 1;
            int mockFreelancerId = 2;
            decimal mockTotalCost = 10000M;

            var project = new Project(mockTitle, mockDescription, mockClientId, mockFreelancerId, mockTotalCost);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            unitOfWorkMock.Setup(x => x.Projects).Returns(projectRepositoryMock.Object);
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result).Returns(project);

            var updateProjectCommand = new UpdateProjectCommand(mockId, "Novo Titulo", "Nova Descricao", 500M);
            var updateProjectCommandHandler = new UpdateProjectCommandHandler(unitOfWorkMock.Object);

            // Act
            await updateProjectCommandHandler.Handle(updateProjectCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.NotEqual(project.Title, mockTitle);
            Assert.NotEqual(project.Description, mockDescription);
            Assert.NotEqual(project.TotalCost, mockTotalCost);
            Assert.Equal(mockClientId, project.IdClient);
            Assert.Equal(mockFreelancerId, project.IdFreelancer);

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id == mockId)).Result, Times.Once);
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.Is<int>(id => id != mockId)).Result, Times.Never);

            unitOfWorkMock.Verify(pr => pr.CompleteAsync(), Times.Once);
        }
    }
}
