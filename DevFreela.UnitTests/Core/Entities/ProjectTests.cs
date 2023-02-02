using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        //[Fact]
        //public void TestIfProjectStartWorks()
        //{
        //    var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

        //    Assert.Equal(ProjectStatusEnum.Created, project.Status);
        //    Assert.Null(project.StartedAt);

        //    Assert.NotNull(project.Title);
        //    Assert.NotEmpty(project.Title);

        //    Assert.NotNull(project.Description);
        //    Assert.NotEmpty(project.Description);

        //    project.Start();

        //    Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        //    Assert.NotNull(project.StartedAt);
        //}

        [Fact]
        public void ProjectIsItStatusCreated_Executed_ChangeStatusToInProgressAndGivenStartDate() // GIVEN_WHEN_THEN
        {
            // Arrange
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);

            // Act
            project.Start();

            // Assert
            Assert.True(project.Status != ProjectStatusEnum.Created);
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.FinishAt);
        }

        [Fact]
        public void ProjectIsItStatusPaymentPending_Executed_ChangeStatusToFinishAndGivenFinishDate() // GIVEN_WHEN_THEN
        {
            // Arrange
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);
            project.Start();
            project.SetPaymentPending();

            // Act
            project.Finish();

            // Assert
            Assert.True(project.Status != ProjectStatusEnum.InProgress);
            Assert.True(project.Status == ProjectStatusEnum.Finish);
            Assert.NotNull(project.StartedAt);
            Assert.NotNull(project.FinishAt);
        }

        [Fact]
        public void ProjectIsItStatusInProgress_Executed_ChangeStatusToCancelled() // GIVEN_WHEN_THEN
        {
            // Arrange
            var project = new Project("Titulo", "Descricao", 1, 2, 10000M);
            project.Start();

            // Act
            project.Cancel();

            // Assert
            Assert.True(project.Status != ProjectStatusEnum.InProgress);
            Assert.True(project.Status == ProjectStatusEnum.Cancelled);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.FinishAt);
        }

        [Fact]
        public void ProjectWithIdNineExists_Executed_ChangeProjectData() // GIVEN_WHEN_THEN
        {
            // Arrange
            string mockTitle = "Titulo";
            string mockDescription = "Descricao";
            int mockClientId = 1;
            int mockFreelancerId = 2;
            decimal mockTotalCost = 10000M;

            var project = new Project(mockTitle, mockDescription, mockClientId, mockFreelancerId, mockTotalCost);

            // Act
            project.Update("Novo Titulo", "Nova Descricao", 500M);

            // Assert
            Assert.NotEqual(project.Title, mockTitle);
            Assert.NotEqual(project.Description, mockDescription);
            Assert.NotEqual(project.TotalCost, mockTotalCost);

            Assert.Equal(mockClientId, project.IdClient);
            Assert.Equal(mockFreelancerId, project.IdFreelancer);
        }
    }
}
