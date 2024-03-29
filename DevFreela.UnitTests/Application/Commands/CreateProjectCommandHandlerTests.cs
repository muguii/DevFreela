﻿using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId() //GIVEN_WHEN_THEN
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var skillRepositoryMock = new Mock<ISkillRepository>();

            unitOfWorkMock.SetupGet(x => x.Projects).Returns(projectRepositoryMock.Object);
            unitOfWorkMock.SetupGet(x => x.Skills).Returns(skillRepositoryMock.Object);

            var createProjectCommand = new CreateProjectCommand()
            {
                Title = "Titulo",
                Description = "Descricao",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 10000M
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWorkMock.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new System.Threading.CancellationToken());

            // Assertions
            Assert.True(id >= 0);

            // Verificando se o AddAsync, para qualquer Project informado, foi chamado uma vez
            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        } 
    }
}
