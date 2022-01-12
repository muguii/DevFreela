using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            return dbContext.Projects.Select(skill => new SkillViewModel(skill.Id, skill.Description)).ToList();
        }
    }
}
