using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            return _dbContext.Skills.Select(skill => new SkillViewModel(skill.Id, skill.Description)).ToList();
        }
    }
}
