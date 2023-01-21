using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsQueryHandlerTests
    {
        [Fact]
        public async Task FiveSkillsExist_Executed_ReturnFiveSkillDTOs() // GIVEN_WHEN_THEN
        {
            // Arrange
            var skills = new List<SkillDTO>()
            {
                new SkillDTO() { Id = 1, Description = "Skill 1" },
                new SkillDTO() { Id = 2, Description = "Skill 2" },
                new SkillDTO() { Id = 3, Description = "Skill 3" },
                new SkillDTO() { Id = 4, Description = "Skill 4" },
                new SkillDTO() { Id = 5, Description = "Skill 5" },
            };

            var skillRepositoryMock = new Mock<ISkillRepository>();
            skillRepositoryMock.Setup(sr => sr.GetAllAsync().Result).Returns(skills);

            var getAllSkillsQuery = new GetAllSkillsQuery();
            var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);

            // Act
            var skillsDTOs = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(skillsDTOs);
            Assert.NotEmpty(skillsDTOs);
            Assert.Equal(skills.Count, skillsDTOs.Count);

            skillRepositoryMock.Verify(sr => sr.GetAllAsync().Result, Times.Once);
        }
    }
}
