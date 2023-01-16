using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                var skills = await sqlConnection.QueryAsync<SkillDTO>("SELECT Id, Description FROM Skills");
                return skills.ToList();
            }

            //return _dbContext.Skills.Select(skill => new SkillViewModel(skill.Id, skill.Description)).ToList();
        }
    }
}
